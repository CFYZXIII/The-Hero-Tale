using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu (menuName = "TileGen/GeneratorSettings")]
public class HexTileGeneratorSettings : ScriptableObject
{
    /// <summary>
    /// ������ �����
    /// </summary>
    public HexTile prefab;

    /// <summary>
    /// ������ �������� ������
    /// </summary>
    [SerializeField] List<TileSettings> settings;

    [SerializeField] Dictionary<int, TileSettings> tileSettings;

    private Dictionary<TileType, TileSettings> dict;

    private Dictionary<TileType, TileSettings> Dict
    {
        get 
        {
            if (dict == null)
            {
                dict = new Dictionary<TileType, TileSettings>();
                foreach (var setting in settings)
                    dict[setting.type] = setting;
            }
            return dict;
        }
    }


    [ExecuteInEditMode]
    public TileSettings this[TileType type]
    {
        get { return Dict[type]; }
    }
}
/// <summary>
/// ����������� ��� ������
/// </summary>
public enum TileType
{
    Default,
    Water,
    Grass
}