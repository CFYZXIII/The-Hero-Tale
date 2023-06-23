using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class HexGrig : MonoBehaviour
{
    [Header("Настрйки стеок")]
    [SerializeField] Vector2 GridSize;
    [SerializeField][Range(0,3)] float CellSize;

    [Header("Ассет настроек")]
    [SerializeField] HexTileGeneratorSettings settings;

    private Vector2 origin => transform.position;
    private const float Y_MODIFIER = 0.85f;


    List<HexTile> tiles = new List<HexTile>();

    #region Методы управления сетками
    [EditorButton("CreateGrid")]
    public void CreateGrid()
    {
        var X1 = Mathf.CeilToInt(GridSize.x/2);
        var X2 = GridSize.x - X1;

        var Y1 = Mathf.CeilToInt(GridSize.y / 2);
        var Y2 = GridSize.y - Y1;



        for (int x = -X1 +1; x <= X2; x++)
        {
            for (int y = -Y1 +1; y <= Y2; y++)
            {
                float dx = x;
                var tile = HexTile.New(settings,transform);

                var tilePosition = origin + new Vector2(CellSize * x, CellSize * y*Y_MODIFIER);
                if (Mathf.Abs(y) % 2 == 1)
                {
                    tilePosition += new Vector2(CellSize / 2, 0);
                    dx += 0.5f;
                }

                tile.transform.localPosition = tilePosition;
                tile.name =  dx+" "+y;
                tiles.Add(tile);
                
            }
        }        
    }

    [EditorButton("ClearGrid")]
    public void ClearGrid()
    {
        foreach (var tile in gameObject.transform.GetComponentsInChildren<Transform>())
        {
            if(!tile.gameObject.Equals(this.gameObject))
            Object.DestroyImmediate(tile.gameObject, true);
        }
        tiles.Clear();
    }
    #endregion

    public static HexGrig Instance;

    private Dictionary<Vector2,Node> NodeCollection = new Dictionary<Vector2, Node>();
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        foreach (var tile in Instance.gameObject.transform.GetComponentsInChildren<Transform>())
        {
            if (!tile.gameObject.Equals(Instance.gameObject))
            {
              var hexTile = tile.gameObject.GetComponent<HexTile>();
                if (hexTile != null)
                {
                    bool walkable = settings[hexTile.TileType].walkable;
                    Instance.NodeCollection[hexTile.PostionInGrid] = new Node(walkable, hexTile.PostionInGrid, hexTile.transform.position);
                }
            }
        }
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

    private HexTile startTile;
    private HexTile endTile;


    private Vector2 mousePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition);

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
            
        //    RaycastHit2D hit = Physics2D.Raycast(mousePosition, mousePosition);

        //    // If it hits something...
        //    if (hit.collider != null)
        //    {
        //        var hextile = hit.collider.gameObject.GetComponent<HexTile>();
        //        if (hextile != null)
        //        {

        //            if (startTile != null)
        //            {
        //                endTile = hextile;
        //                //endTile.SetColor();
        //                //GetDistance(endTile,startTile);
        //            }

        //            if (startTile == null)
        //            {
        //                startTile = hextile;
        //                //startTile.SetColor();
        //            }
                    
        //        }
        //    }
        //}

        //if (Input.GetMouseButtonDown(1))
        //{
        //    startTile?.ReSetColor();
        //    endTile?.ReSetColor();
        //    startTile = endTile = null;
        //}
    }


    public void GetAllNeighbors(Node node, Node endNode, List<Node> openList, List<Node> closedList)
    {        
        foreach (var pos in neighborPositions)
        {
            var position = pos + node.tilePos;
            var neighborNode = GetNode(position);
            if (neighborNode != null && neighborNode.walkable && !closedList.Contains(neighborNode))
            {
                //тут возможно просто +1 вместо дистанции
                neighborNode.CalculateCosts(node.GCost + node.Distance(neighborNode), endNode);
                neighborNode.previousNode = node;
                openList.Add(neighborNode);
                closedList.Add(neighborNode);
            }
        }

        openList.Remove(node);
    }

    private Node GetNode(Vector2 position)
    {
        if (NodeCollection.ContainsKey(position))
            return NodeCollection[position];
        return null;
    }

    public Node GetTileNode(Vector2 position)
    {
        RaycastHit2D hit = Physics2D.Raycast(position, position);
        if (hit.collider != null)
        {
            var hextile = hit.collider.gameObject.GetComponent<HexTile>();
            if (hextile != null)
            {
                return NodeCollection[hextile.PostionInGrid];
            }
        }
        return null;
    }




    //public int GetDistance(HexTile endTile, HexTile startTile)
    //{
    //    Vector2 startPosition = startTile.PostionInGrid;
    //    Vector2 endPosition = endTile.PostionInGrid;

    //    int nodeCount = 0;

    //    while(endPosition.y!=startPosition.y)
    //    {
    //        //Расстояние
    //        Vector2 distance = endPosition - startPosition;
    //        //Направление
    //        Vector2 direction = new Vector2(Mathf.Sign(distance.x), Mathf.Sign(distance.y));
    //        //Приращение
    //        Vector2 dxy = new Vector2(0.5f, 1) * direction;
    //        startPosition += dxy;
    //        nodeCount++;
    //    }

    //    nodeCount += (int)Mathf.Abs(endPosition.x-startPosition.x);

    //    Debug.Log(nodeCount);
    //    return nodeCount;

    //}

    public List<Vector2> neighborPositions = new List<Vector2>()
    {
        new Vector2(-0.5f,1),
        new Vector2(0.5f,1),
        new Vector2(1,0),
        new Vector2(0.5f,-1),
        new Vector2(-0.5f,-1),
        new Vector2(-1,0),
    };
}
