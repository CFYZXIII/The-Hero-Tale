using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HexGrig : MonoBehaviour
{
    [Header("GridSettings")]
    [SerializeField] Vector2 GridSize;
    [SerializeField][Range(0,3)] private float CellSize;


    [SerializeField] HexTile prefab;

    private Vector2 origin => transform.position;
    private const float Y_MODIFIER = 0.85f;


    List<HexTile> tiles = new List<HexTile>();

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
                var tile = Instantiate(prefab);
                tile.transform.parent = transform;

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

    private Dictionary<Vector2,HexTile> tileDict = new Dictionary<Vector2,HexTile>();
    private void Start()
    {
        foreach (var tile in gameObject.transform.GetComponentsInChildren<Transform>())
        {
            if (!tile.gameObject.Equals(this.gameObject))
            {
              var hexTile = tile.gameObject.GetComponent<HexTile>();
                if(hexTile!=null)
                    tileDict[hexTile.PostionInGrid] = hexTile;
            }
        }
    }


    private HexTile startTile;
    private HexTile endTile;


    private Vector2 mousePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition);

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, mousePosition);

            // If it hits something...
            if (hit.collider != null)
            {
                var hextile = hit.collider.gameObject.GetComponent<HexTile>();
                if (hextile != null)
                {

                    if (startTile != null)
                    {
                        endTile = hextile;
                        endTile.SetColor();
                        FindPath(endTile,startTile);
                    }

                    if (startTile == null)
                    {
                        startTile = hextile;
                        startTile.SetColor();
                    }
                    
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            startTile?.ReSetColor();
            endTile?.ReSetColor();
            startTile = endTile = null;
        }
    }

    public void FindPath(HexTile endTile, HexTile startTile)
    {
        Vector2 startPosition = startTile.PostionInGrid;
        Vector2 endPosition = endTile.PostionInGrid;




        int nodeCount = 0;

        while(endPosition.y!=startPosition.y)
        {
            //Расстояние
            Vector2 distance = endPosition - startPosition;
            //Направление
            Vector2 direction = new Vector2(Mathf.Sign(distance.x), Mathf.Sign(distance.y));
            //Приращение
            Vector2 dxy = new Vector2(0.5f, 1) * direction;
            startPosition += dxy;
            nodeCount++;
        }

        nodeCount += (int)Mathf.Abs(endPosition.x-startPosition.x);

        Debug.Log(nodeCount);
        
        
    }
}
