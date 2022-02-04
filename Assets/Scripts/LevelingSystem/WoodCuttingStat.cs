using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodCuttingStat : MonoBehaviour
{
    public int woodCuttingLevel = 1;
    public int currentEXP;
    public int[] expToNextLevel;
    public int maxLevel = 5;
    public int baseEXP = 1000;

   // WoodCuttingStat w;
  

    // Start is called before the first frame update
    void Start()
    {
        expToNextLevel = new int[maxLevel];
        expToNextLevel[1] = baseEXP;

        for (int i = 2; i < expToNextLevel.Length; i++)
        {
            expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.05f);
        }

    }

    // Update is called once per frame
    void Update()
    {
        // CropDetails c = new CropDetails();

        //if (//tree is cut down)
        //{
        //    AddExp(c.xp); 
        //}

        //if (c.seedItemCode == 10000)
        //{
        //    if ()
        //    {
        //        AddExp(c.xp);
        //    }
        //    else
        //    {

        //    }

        //}
        ItemDetails i = new ItemDetails();

       // InventoryManager inventory = new InventoryManager();

        //inventory.GetItemDetails(10007);
      //  AddExp(i.xp);
        //if ()
        //{
        //    if ()
        //    {
        //        AddExp(i.xp);
        //    }
        //    else
        //    {
        //        AddExp(500);
        //    }
        //}

        if (Input.GetKeyDown(KeyCode.Q))
        {
            AddExp(500);
            //AddExp(c.xp);
        }
    }


    public void AddExp(int expToAdd)
    {
        currentEXP += expToAdd;

        if (woodCuttingLevel < maxLevel)
        {
            if (currentEXP > expToNextLevel[woodCuttingLevel])
            {
                currentEXP -= expToNextLevel[woodCuttingLevel];

                woodCuttingLevel++;

            }
        }

        if (woodCuttingLevel >= maxLevel)
        {
            currentEXP = 0;
        }
    }




}
