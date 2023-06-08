using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Node 
{
    /// <summary>
    /// ����, ������������ ����� �� ������ �� �����
    /// </summary>
    public bool walkable;
    /// <summary>
    /// ��������� ��������� � ���� ���
    /// </summary>
    public double GCost;
    /// <summary>
    /// ��������� ��������� �� ����� ���� � ��������� ���
    /// </summary>
    public double HCost;
    /// <summary>
    /// ��������� ���������
    /// </summary>
    public double FCost;
    /// <summary>
    /// ���, �� ������� ������
    /// </summary>
    public Node previousNode;
    /// <summary>
    /// ������� ���� �� �����
    /// </summary>
    public Vector2Int tilePos;
    /// <summary>
    /// ������� ���� � ���� ����
    /// </summary>
    public Vector2 transformPos;



    public Node(bool walkable, Vector2Int tilePos, Vector2 transformPos)
    {
        GCost = HCost = FCost = Mathf.Infinity;
        this.tilePos = tilePos;
        this.transformPos = transformPos;
        this.walkable = walkable;
    }

    public float Distance(Node node)
    {
        return Vector2.Distance(this.transformPos,node.transformPos);
    }

}
