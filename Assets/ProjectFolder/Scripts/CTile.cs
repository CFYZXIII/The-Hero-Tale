using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;



[CreateAssetMenu(fileName = "CTile", menuName = "2D/Tiles/CTile", order = 1)]
public class CTile : Tile
{
    /// <summary>
    /// флаг, показывающий можно ди ходить по тайлу
    /// </summary>
    public bool walkable;
    
    
}
