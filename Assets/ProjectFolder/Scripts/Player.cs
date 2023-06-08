using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;
using System.Linq;

public class Player : MonoBehaviour
{
    [SerializeField] private Tilemap map;
    [SerializeField] private CTile tile;

    private CTile startTile;
    private Vector2 startPosition;



    List<Node> openList = new List<Node>();
    List<Node> closedList = new List<Node>();

    private void Start()
    {

    }

    public Vector2 MousePos
    {
        get
        {
          return  Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var endNode = CTileMap.Instance.GetNode(MousePos);

            var startNode = CTileMap.Instance.GetNode(transform.position);
            startNode.GCost = 0;
            startNode.HCost = System.Math.Round(startNode.Distance(endNode), 2);
            startNode.FCost = startNode.HCost + startNode.GCost;
            openList.Add(startNode);
            closedList.Add(startNode);

            Node currentNode = null;

            while (openList.Count > 0)
            {
                currentNode = openList.OrderBy(x=>x.FCost).First();
                CTileMap.Instance.DrawMayBe(currentNode);
                if (currentNode.Equals(endNode))
                    break;
                CTileMap.Instance.GetAllNeighbors(currentNode, endNode, openList,closedList);
               
            }

            CTileMap.Instance.DrawPath(currentNode);


           // double dist2 = System.Math.Round( startNode.Distance(endNode),2);

           // Debug.Log(currentNode);



            //Положение
            

        }

        

    }

    
}
