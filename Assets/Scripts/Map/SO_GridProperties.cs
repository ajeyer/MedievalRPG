using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "so_GridProperties", menuName = "Scriptable Objects/Objects/Grid Properties")]

public class SO_GridProperties : ScriptableObject
{
    public SceneName sceneName;
    public int gridWidth;
    public int gridHeight;
    public int orginX;
    public int orginY;

    [SerializeField]
    public List<GridProperty> gridPropertyList;

}
