using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIInventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Camera mainCamera;
    private Canvas parentCanvas;
    private Transform parentItem;
    private GridCursor gridCursor;
    private Cursor cursor;
    private GameObject draggedItem;

    public Image inventorySlotHighlight;
    public Image inventonrySlotImage;
    public TextMeshProUGUI textMeshProUGUI;

    [SerializeField] private UIInventoryBar inventoryBar = null;
    [SerializeField] private GameObject inventoryTextBoxPrefab = null;
    [HideInInspector] public bool isSelected = false;
    [HideInInspector] public ItemDetails itemDetails;
    [SerializeField] private GameObject itemPrefab = null;
    [HideInInspector] public int itemQuantity;
    [SerializeField] private int slotNumber = 0;


    private void Awake()
    {
        parentCanvas = GetComponentInParent<Canvas>();
    }


    private void OnDisable()
    {
        EventHandler.AfterSceneLoadEvent -= SceneLoaded;
        EventHandler.RemoveSelectedItemFromInventoryEvent += RemoveSelectedItemFromInventory;
        EventHandler.DropSelectedItemEvent += DropSelectedItemAtMousePosition;
    }


    private void OnEnable()
    {
        EventHandler.AfterSceneLoadEvent += SceneLoaded;
        EventHandler.RemoveSelectedItemFromInventoryEvent += RemoveSelectedItemFromInventory;
        EventHandler.DropSelectedItemEvent += DropSelectedItemAtMousePosition;
    }


    private void Start()
    {
        mainCamera = Camera.main;
        gridCursor = FindObjectOfType<GridCursor>();
        cursor = FindObjectOfType<Cursor>();
    }


    private void ClearCursor()
    {
        //Disable Cursor
        gridCursor.DisableCursor();
        cursor.DisableCursor();

        //Set item type to none
        gridCursor.SelectedItemType = ItemType.none;
        cursor.SelectedItemType = ItemType.none;
    }


    /// <summary>
    /// Sets this inventory slot item to be selected
    /// </summary>
    private void SetSelectedItem()
    {
        //Clear currently highlighted items
        inventoryBar.ClearHighlightOnInventorySlots();

        //Highlight item on inventory bar
        isSelected = true;

        //Set highlighted inventory slots
        inventoryBar.SetHighlightedInventorySlots();

        //Set use radius for cursors
        gridCursor.ItemUseGridRadius = itemDetails.itemUseGridRadius;
        cursor.ItemUseRadius = itemDetails.itemUseRadius;

        //If item requires a grid cursor then enable cursor
        if (itemDetails.itemUseGridRadius > 0)
        {
            gridCursor.EnableCursor();
        }
        else
        {
            gridCursor.DisableCursor();
        }

        //If item requires a cursor then enable cursor
        if (itemDetails.itemUseRadius > 0f)
        {
            cursor.EnableCursor();
        }
        else
        {
            cursor.DisableCursor();
        }


        //Set item type
        gridCursor.SelectedItemType = itemDetails.itemType;
        cursor.SelectedItemType = itemDetails.itemType;

        //Set iten selected in inventory
        InventoryManager.Instance.SetSelectedInventoryItem(InventoryLocation.player, itemDetails.itemCode);

        if (itemDetails.canBeCarried == true)
        {
            //Show player carrying item
            Player.Instance.ShowCarriedItem(itemDetails.itemCode);
        }
        else // Show player carrying nothing
        {
            Player.Instance.ClearCarriedItem();
        }
    }

   private void ClearSelectedItem()
    {
        ClearCursor();

        //Clear currently highlighted items
        inventoryBar.ClearHighlightOnInventorySlots();

        isSelected = false;

        //Set no item selected to inventory
        InventoryManager.Instance.ClearSelectedInventoryItem(InventoryLocation.player);

        //Clear player carrying item
        Player.Instance.ClearCarriedItem();
    }

    private void DropSelectedItemAtMousePosition()
    {
        if (itemDetails != null && isSelected)
        {

            //If a valid cursor positoin
            if (gridCursor.CursorPositionIsValid)
            { 
                Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -mainCamera.transform.position.z));

                //Create item from prefab at mouse position
                GameObject itemGameObject = Instantiate(itemPrefab, new Vector3(worldPosition.x, worldPosition.y - Settings.gridCellSize/2, worldPosition.z), Quaternion.identity, parentItem);
                Item item = itemGameObject.GetComponent<Item>();
                item.ItemCode = itemDetails.itemCode;

                //Remove item from players inventory
                InventoryManager.Instance.RemoveItem(InventoryLocation.player, item.ItemCode);

                //If no more of item then clear selected
                if (InventoryManager.Instance.FindItemInInventory(InventoryLocation.player, item.ItemCode) == -1)
                {
                    ClearSelectedItem();
                }

            }
        }
       // ClearCursor();
    }


    private void RemoveSelectedItemFromInventory()
    {
        if(itemDetails!=null && isSelected)
        {
            int itemCode = itemDetails.itemCode;

            //Remove item from players inventory
            InventoryManager.Instance.RemoveItem(InventoryLocation.player, itemCode);

            //If no more of item then clear selected
            if (InventoryManager.Instance.FindItemInInventory(InventoryLocation.player, itemCode) == -1)
            {
                ClearSelectedItem();
            }
        }
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (itemDetails != null)
        {
            //Disable keyboard input
            Player.Instance.DisablePlayerInputAndResetMovement();

            //Instantiate gameobject as dragged item
            draggedItem = Instantiate(inventoryBar.inventoryBarDraggedItem, inventoryBar.transform);

            //Get image for dragged item
            Image draggedItemImage = draggedItem.GetComponentInChildren<Image>();
            draggedItemImage.sprite = inventonrySlotImage.sprite;

            SetSelectedItem();

        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Move game object as dragged item
        if (draggedItem != null)
        {
            draggedItem.transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Destroy game object as dragged item
        if (draggedItem != null)
        {
            Destroy(draggedItem);

            //If drag end over inventory bar, get item drag is over and swaps them
            if(eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.GetComponent<UIInventorySlot>() != null)
            {
                //Get the slot number where the drag ended
                int toSlotNumber = eventData.pointerCurrentRaycast.gameObject.GetComponent<UIInventorySlot>().slotNumber;

                //Swap inventory items in inventory list
                InventoryManager.Instance.SwapInventoryItem(InventoryLocation.player, slotNumber, toSlotNumber);

                //Destroy inventory text box
                DestroyInventoryTextBox();

                //Clear Selected item
                ClearSelectedItem();
            }
            else
            {
                //Else attempt to drop the item if it can be dropped
                if (itemDetails.canBeDropped)
                {
                    DropSelectedItemAtMousePosition();
                }
            }

            //Enable player input
            Player.Instance.EnablePlayerInput();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //If left click
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            //If inventory slot currently selected then deselect
            if(isSelected == true)
            {
                ClearSelectedItem();
            }
            else
            {
                if(itemQuantity > 0)
                {
                    SetSelectedItem();
                }
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Populate text box with item details
        if (itemQuantity != 0)
        {
            //Instantiate inventory text box
            inventoryBar.inventoryTextBoxGameObject = Instantiate(inventoryTextBoxPrefab, transform.position, Quaternion.identity);
            inventoryBar.inventoryTextBoxGameObject.transform.SetParent(parentCanvas.transform, false);

            UIInventoryTextBox inventoryTextBox = inventoryBar.inventoryTextBoxGameObject.GetComponent<UIInventoryTextBox>();

            //Set item type description
            string itemTypeDescription = InventoryManager.Instance.GetItemTypeDescription(itemDetails.itemType);

            //Populate text box
            inventoryTextBox.SetTextBoxText(itemDetails.itemDescription, itemTypeDescription, "", itemDetails.itemLongDescription, "", "");

            //Set text box position according to inventory bar bar position
            if (inventoryBar.IsInventoryBarPositionBottom)
            {
                inventoryBar.inventoryTextBoxGameObject.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0f);
                inventoryBar.inventoryTextBoxGameObject.transform.position = new Vector3(transform.position.x, transform.position.y + 50f, transform.position.z);
            }
            else
            {
                inventoryBar.inventoryTextBoxGameObject.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 1f);
                inventoryBar.inventoryTextBoxGameObject.transform.position = new Vector3(transform.position.x, transform.position.y - 50f, transform.position.z);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DestroyInventoryTextBox();
    }

    public void DestroyInventoryTextBox()
    {
        if(inventoryBar.inventoryTextBoxGameObject != null)
        {
            Destroy(inventoryBar.inventoryTextBoxGameObject);
        }
    }


    public void SceneLoaded()
    {
        parentItem = GameObject.FindGameObjectWithTag(Tags.ItemsParentTransform).transform;
    }
}
