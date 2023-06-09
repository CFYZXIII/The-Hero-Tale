using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Node 
{
    /// <summary>
    /// флаг, показывающий можно ди ходить по тайлу
    /// </summary>
    public bool walkable;
    /// <summary>
    /// Стоимость попадания в этот нод
    /// </summary>
    public double GCost;
    /// <summary>
    /// Стоимость попадания из этого нода в финальный нод
    /// </summary>
    public double HCost;
    /// <summary>
    /// Суммарная стоимость
    /// </summary>
    public double FCost;
    /// <summary>
    /// Нод, из которго пришли
    /// </summary>
    public Node previousNode;
    /// <summary>
    /// Позиция нода на сетке
    /// </summary>
    public Vector2Int tilePos;
    /// <summary>
    /// Позиция нода в мире инры
    /// </summary>
    public Vector2 transformPos;
    /// <summary>
    /// угол поворота, при прохождении через нод
    /// </summary>
    public float algle;



    public Node(bool walkable, Vector2Int tilePos, Vector2 transformPos)
    {
        HCost = FCost = Mathf.Infinity;
        GCost = 0;
        this.tilePos = tilePos;
        this.transformPos = transformPos;
        this.walkable = walkable;
    }

    public float Distance(Node node)
    {
        return Vector2.Distance(this.transformPos,node.transformPos);
    }

    public void CalculateCosts(double GCost, Node endNode)
    {
        this.GCost = GCost;
        HCost = System.Math.Round(Distance(endNode), 2);
        FCost = HCost + GCost;
    }

}
