
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Node 
{
    /// <summary>
    /// ����, ������������ ����� �� ������ �� �����
    /// </summary>
    public bool walkable;
    /// <summary>
    /// ��������� ��������� � ���� ���
    /// </summary>
    public int GCost;
    /// <summary>
    /// ��������� ��������� �� ����� ���� � ��������� ���
    /// </summary>
    public int HCost;
    /// <summary>
    /// ��������� ���������
    /// </summary>
    public int FCost;
    /// <summary>
    /// ���, �� ������� ������
    /// </summary>
    public Node previousNode;
    /// <summary>
    /// ������� ���� �� �����
    /// </summary>
    public Vector2 tilePos;
    /// <summary>
    /// ������� ���� � ���� ����
    /// </summary>
    public Vector2 transformPos;
    /// <summary>
    /// ���� ��������, ��� ����������� ����� ���
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
            //����������
            Vector2 distance = endPosition - startPosition;
            //�����������
            Vector2 direction = new Vector2(Mathf.Sign(distance.x), Mathf.Sign(distance.y));
            //����������
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
