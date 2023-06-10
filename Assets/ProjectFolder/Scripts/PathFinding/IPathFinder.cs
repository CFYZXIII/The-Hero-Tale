using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPathFinder 
{
    /// <summary>
    /// 
    /// </summary>
    public Vector2 EndNodePosition { get; }

    public void FindPath();


}
