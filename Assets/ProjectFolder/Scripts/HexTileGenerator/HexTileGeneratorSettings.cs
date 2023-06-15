using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "TileGen/GeneratorSettings")]
public class HexTileGeneratorSettings : ScriptableObject
{

    public enum TileType
    {
        Water,
        Grass
    }

    public GameObject Water;
    public GameObject Grass;

    public GameObject GetTile(TileType type)
    {
        switch (type)
        {
            case TileType.Water:
                return null;
            case TileType.Grass:
                return null;
            default: return null;
        }
    }

}
