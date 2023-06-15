using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class HexTile : MonoBehaviour
{



    public void SetColor()
    {
        var sr = GetComponent<SpriteRenderer>();
        sr.color = Color.green;
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
