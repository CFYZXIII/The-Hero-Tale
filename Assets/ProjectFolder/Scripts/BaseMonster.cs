using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMonster : PathFinder
{
    public override Vector2 EndNodePosition => PlayerNode==null?Vector2.zero:PlayerNode.transformPos;

    /// <summary>
    /// Нод, текущего положения мгрока
    /// </summary>
    private Node PlayerNode = null;

    private void Start()
    {
        OnTurnEnd += BaseMonster_OnTurnEnd;       
    }

    private void BaseMonster_OnTurnEnd(IPathFinder pathFinder, Node node)
    {
        if (!moved)
        {
            MovePoints = BaseMovePoints;
            PlayerNode = node;
            FindPath();
            ClearPathNodes();
            GoMove();
        }
    }
}
