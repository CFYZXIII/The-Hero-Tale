using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TileGen/TileSettings")]
public class TileSettings : ScriptableObject
{
    /// <summary>
    /// ������ �������� �����
    /// </summary>
    public Sprite sprite;

    /// <summary>
    /// (����) ���� �����
    /// </summary>
    public Color color;

    /// <summary>
    /// ������������ �����
    /// </summary>
    public bool walkable;

    /// <summary>
    /// ��� �������� �����
    /// </summary>
    public TileType type;

}
