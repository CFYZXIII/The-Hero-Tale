
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Node 
{
    /// <summary>
    /// флаг, показывающий можно ди ходить по тайлу
    /// </summary>
    public bool walkable;
    /// <summary>
    /// Стоимость попадания в этот нод
    /// </summary>
    public int GCost;
    /// <summary>
    /// Стоимость попадания из этого нода в финальный нод
    /// </summary>
    public int HCost;
    /// <summary>
    /// Суммарная стоимость
    /// </summary>
    public int FCost;
    /// <summary>
    /// Нод, из которго пришли
    /// </summary>
    public Node previousNode;
    /// <summary>
    /// Позиция нода на сетке
    /// </summary>
    public Vector2 tilePos;
    /// <summary>
    /// Позиция нода в мире инры
    /// </summary>
    public Vector2 transformPos;
    /// <summary>
    /// угол поворота, при прохождении через нод
    /// </summary>

    public Node(bool walkable, Vector2 tilePos, Vector2 transformPos)
    {
        HCost = FCost = int.MaxValue;
        GCost = 0;
        this.tilePos = tilePos;
        this.transformPos = transformPos;
        this.walkable = walkable;
    }

    public int Distance(Node node)
    {
        Vector2 startPosition = tilePos;
        Vector2 endPosition = node.tilePos;

        int nodeCount = 0;

        while (endPosition.y != startPosition.y)
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

        nodeCount += (int)Mathf.Abs(endPosition.x - startPosition.x);

       
        return nodeCount;
    }

    public void CalculateCosts(int GCost, Node endNode)
    {
        this.GCost = GCost;
        HCost = Distance(endNode);
        FCost = HCost + GCost;
    }

}
