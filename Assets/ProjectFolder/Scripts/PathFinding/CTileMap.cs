using System.Collections.Generic;
using System.IO;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.PlayerSettings;

public class CTileMap : MonoBehaviour
{
    
    //[SerializeField] Transform target;

    public static CTileMap Instance;
    private Tilemap tilemap;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            NodeCollection = new Dictionary<Vector2Int, Node>();
        }

        tilemap = GetComponent<Tilemap>();

        BoundsInt bounds = tilemap.cellBounds;

        Vector2Int startPosition = (Vector2Int)tilemap.cellBounds.position;

        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                var tilePos = startPosition + new Vector2Int(x, y);
                CTile tile = tilemap.GetTile<CTile>((Vector3Int)tilePos);
                if (tile != null)
                {
                    Node node = new Node(tile.walkable, tilePos, (Vector2)tilemap.CellToWorld((Vector3Int)tilePos));
                    NodeCollection[tilePos] = node;
                }

            }
        }

        
    }

    public Node GetNode(Vector3 position)
    {
        Vector2Int tilePos = (Vector2Int)tilemap.WorldToCell(position);
        if (NodeCollection.ContainsKey(tilePos))
            return NodeCollection[tilePos];
        return null;
    }

    private Node GetNode(Vector2Int tilePos)
    {
        if (NodeCollection.ContainsKey(tilePos))
            return NodeCollection[tilePos];
        return null;
    }

    public Dictionary<Vector2Int, Node> NodeCollection;



    public void GetAllNeighbors(Node node,Node endNode, List<Node> openList, List<Node> closedList)
    {
        var NeighborPositions = CalculateNeighborsPositions(node.tilePos);
        foreach (var position in NeighborPositions)
        {
            var neighborNode = GetNode(position);
            if (neighborNode != null && neighborNode.walkable && !closedList.Contains(neighborNode))
            {
                neighborNode.CalculateCosts(node.GCost + System.Math.Round(node.Distance(neighborNode), 2), endNode);

                neighborNode.previousNode = node;
                openList.Add(neighborNode);
                closedList.Add(neighborNode);
            }
        }

        openList.Remove(node);
    }

    public List<Vector2Int> CalculateNeighborsPositions(Vector2Int tilepos)
    {
        var positions = new List<Vector2Int>();
        // верхний
        positions.Add(new Vector2Int(1, 0) + tilepos);
        //нижнbй
        positions.Add(new Vector2Int(-1, 0) + tilepos);
        //левый 1
        positions.Add(new Vector2Int(0, -1) + tilepos);
        //правый 1
        positions.Add(new Vector2Int(0, 1) + tilepos);

        int corrector = Mathf.Abs(tilepos.y) % 2 == 0 ? -1 : 1;
        //левый 2
        positions.Add(new Vector2Int(corrector, -1) + tilepos);
        //правый 2
        positions.Add(new Vector2Int(corrector, 1) + tilepos);

        return positions;
    }

   
    public List<Node> GetPositions(Node node)
    {        
        List<Node> nodes = new List<Node>();

        while (node.previousNode != null)
        {            
            nodes.Add(node);
            node = node.previousNode;
        }
        nodes.Reverse();
        return nodes;
    }   

}