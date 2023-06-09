using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;


public class PathFinder :MonoBehaviour, IPathFinder
{
    /// <summary>
    /// Позиция целевого нода
    /// </summary>
    public virtual Vector2 EndNodePosition => Vector3.zero;
    /// <summary>
    /// Открытый список нодов
    /// </summary>
    private List<Node> openList = new List<Node>();
    /// <summary>
    /// Закрытый список нодов
    /// </summary>
    private List<Node> closedList = new List<Node>();
    /// <summary>
    /// Ноды посторения пути
    /// </summary>
    /// 
    public List<Node> pathNodes; 
    /// <summary>
    ///Стартовые Очки движения
    /// </summary>
    [SerializeField]
    protected int BaseMovePoints;
    /// <summary>
    /// Очки движения
    /// </summary>
    protected int MovePoints;



    
    /// <summary>
    /// Метод поиска пути
    /// </summary>
    /// <returns></returns>
    public virtual List<Node> FindPath()
    {

        Node endNode = CTileMap.Instance.GetNode(EndNodePosition);
        if (endNode != null)
        {
            BeforePathFinding();
            pathNodes = new List<Node>();
            var startNode = CTileMap.Instance.GetNode(transform.position);
            startNode.CalculateCosts(0, endNode);

            openList.Add(startNode);
            closedList.Add(startNode);

            Node currentNode = null;

            while (openList.Count > 0)
            {
                currentNode = openList.OrderBy(x => x.FCost).First();

                if (currentNode.Equals(endNode))
                    break;
                CTileMap.Instance.GetAllNeighbors(currentNode, endNode, openList, closedList);

            }
            pathNodes = CTileMap.Instance.GetPositions(currentNode);
            AfterPathFinding();

        }
        return pathNodes;
    }

    /// <summary>
    /// Метод, которы выполнится до нахождения пути
    /// </summary>
    public virtual void BeforePathFinding() { }

    /// <summary>
    /// Метод, которы выполнится после нахождения пути
    /// </summary>
    public virtual void AfterPathFinding() { }


    /// <summary>
    /// Метод, очищабщий все ноды
    /// </summary>
    public void ClearPathNodes()
    {
        closedList.AddRange(openList);
        foreach (var node in closedList)
            node.previousNode = null;

        openList.Clear();
        closedList.Clear();
    }

    /// <summary>
    /// Метод для отрисовки пути
    /// </summary>
    /// <param name="node"></param>
    /// <param name="i"></param>
    protected void DrawPath( List<Transform> pathArrows)
    {
        for (int i = pathNodes.Count - 1; i >= 0; i--)
        {
            Transform arrow = null;

            Vector2 d = pathNodes[i].transformPos - pathNodes[i].previousNode.transformPos;

            var angle = Mathf.Atan2(d.y, d.x) * 180 / Mathf.PI;
            pathNodes[i].algle = angle;
            if (i < pathArrows.Count - 1)
            {
                arrow = pathArrows[i];
            }
            else
            {
                arrow = Instantiate(pathArrows[0]);
                pathArrows.Add(arrow);
            }

            arrow.gameObject.SetActive(true);
           
            arrow.rotation = Quaternion.Euler(0, 0, angle);
            arrow.position = pathNodes[i].transformPos;
        }
    }

    /// <summary>
    /// Метод очистки стрелок пути
    /// </summary>
    protected void ClearPathArrows(List<Transform> pathArrows)
    {
        foreach (var p in pathArrows)
        {
            p.gameObject.SetActive(false);
        }
    }

}
