using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TileGen/TileSettings")]
public class TileSettings : ScriptableObject
{
    /// <summary>
    /// Спрайт будущего тайлы
    /// </summary>
    public Sprite sprite;

    /// <summary>
    /// (ТЕст) цвет тайла
    /// </summary>
    public Color color;

    /// <summary>
    /// Проходимость тайла
    /// </summary>
    public bool walkable;

    /// <summary>
    /// Тип будущего тайла
    /// </summary>
    public TileType type;

}
