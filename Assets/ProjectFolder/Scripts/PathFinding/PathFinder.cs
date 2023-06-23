
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Player;


public class PathFinder : MonoBehaviour, IPathFinder
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
    /// текущий шаг
    /// </summary>
    protected int curentStep;
    //тут наверное воид лучше
    /// <summary>
    /// Метод поиска пути
    /// </summary>
    /// <returns></returns>
    public virtual void FindPath()
    {

        Node endNode = HexGrig.Instance.GetTileNode(EndNodePosition);
        if (endNode != null)
        {
            curentStep = 0;
            BeforePathFinding();
            pathNodes = new List<Node>();
            var startNode = HexGrig.Instance.GetTileNode(transform.position);
            startNode.CalculateCosts(0, endNode);

            openList.Add(startNode);
            closedList.Add(startNode);

            Node currentNode = null;

            while (openList.Count > 0)
            {
                currentNode = openList.OrderBy(x => x.FCost).First();

                if (currentNode.Equals(endNode))
                    break;
                HexGrig.Instance.GetAllNeighbors(currentNode, endNode, openList, closedList);

            }
            pathNodes = HexGrig.Instance.GetPositions(currentNode);
            AfterPathFinding();

        }

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
    protected void DrawPath(List<Transform> pathArrows)
    {
        for (int i = pathNodes.Count - 1; i >= 0; i--)
        {
            Transform arrow = null;

            var angle = Angle(pathNodes[i].transformPos, pathNodes[i].previousNode.transformPos);
            
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

    protected float Angle(Vector2 t1, Vector2 t2)
    {
        Vector2 d = t1 - t2;

        return  Mathf.Atan2(d.y, d.x) * 180 / Mathf.PI;
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

    protected bool moved;
    /// <summary>
    /// Переделать на дотвин
    /// </summary>
    /// <param name="curentStep"></param>
    /// <returns></returns>
    protected IEnumerator Move()
    {
        yield return null;
        if (curentStep < pathNodes.Count && MovePoints > 0)
        {
            moved = true;
            var angle = Angle(pathNodes[curentStep].transformPos, transform.position);
            while (((Vector2)transform.position - pathNodes[curentStep].transformPos).magnitude > 0.001f)
            {
                transform.position = Vector2.MoveTowards(transform.position, pathNodes[curentStep].transformPos, 0.01f);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), 0.1f);
                yield return new WaitForSeconds(0.01f);
            }
            //Событие на каждый шаг
            OnStepEnd?.Invoke();

            StartCoroutine(Move());
            curentStep++;
            MovePoints--;
        }
        else
        {
            moved = false;
        }

    }

   
       


    //тут различные события, пока там, потом возможно просто на делегаты поменяю

    /// <summary>
    /// Событие, вызываемое в конце хода
    /// </summary>
    public delegate void OnTurnEnded(IPathFinder pathFinder,Node node);
    public static event OnTurnEnded OnTurnEnd;

    public delegate void OnStepEnded();
    public event OnStepEnded OnStepEnd;

    protected void BeforeNewTurn()
    {
        moved = false;
        OnTurnEnd?.Invoke(this, pathNodes[curentStep-1]);
    }

    public void GoMove()
    {
        StartCoroutine(Move());
    }


}
