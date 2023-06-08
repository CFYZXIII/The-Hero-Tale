using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NodeCollection: Dictionary<Vector3Int,Node> 
{
    private static NodeCollection instance;
    public static NodeCollection Collection
    {
        get 
        { 
            if (instance == null)   
                instance = new NodeCollection();
            return instance; 
        }
    }
}
