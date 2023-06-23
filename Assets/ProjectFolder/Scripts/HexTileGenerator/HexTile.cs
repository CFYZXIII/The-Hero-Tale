using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class HexTile : MonoBehaviour
{
   [SerializeField] private TileType type;

    public TileType TileType => type;

    public static HexTile New(HexTileGeneratorSettings settings, Transform parent)
    {
        var tile =  Instantiate(settings.prefab);
        tile.settings = settings;
        tile.transform.parent = parent;
        return tile;
    }


    [SerializeField] private HexTileGeneratorSettings settings;
    public void OnValidate()
    {
        if (!Application.isPlaying)
        {
            Debug.Log("try changed " + settings != null);
            if ( settings != null)
            {
                Debug.Log("changed");
                SetColor(settings);
            }
        }
    }


    


    public void SetColor(HexTileGeneratorSettings settings)
    {
        Debug.Log("Color set");
        var sr = GetComponent<SpriteRenderer>();
        sr.color = settings[type].color;
    }

    public void ReSetColor()
    {
        var sr = GetComponent<SpriteRenderer>();
        sr.color = Color.white;
    }

    private Vector2 postionInGrid = new Vector2(0.1f,0.1f);

    public Vector2 PostionInGrid
    {
        get 
        {
            if (postionInGrid.Equals(new Vector2(0.1f, 0.1f)))
            {
                var coords = name.Split(' ');
                postionInGrid = new Vector2(float.Parse(coords[0]), float.Parse(coords[1]));
            }
            return postionInGrid;
        }
    }


}
