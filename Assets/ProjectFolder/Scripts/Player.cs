using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : PathFinder
{
    /// <summary>
    /// стрелка пути
    /// </summary>
    [SerializeField] Transform arrow;
    /// <summary>
    /// Список стрело пути
    /// </summary>
    private List<Transform> pathArrows = new List<Transform>();

    public override Vector2 EndNodePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition);

    public override void BeforePathFinding()
    {
        ClearPathArrows(pathArrows);
    }

    public override void AfterPathFinding()
    {
        DrawPath(pathArrows);
    }

    private void Start()
    {
        MovePoints = BaseMovePoints;
        for (int i = 0; i < 10; i++)
        {
            var path = Instantiate(arrow);
            path.gameObject.SetActive(false);
            pathArrows.Add(path);
        }

        //OnTurnEnd += DDD;
        OnStepEnd += Player_OnStepEnd;

    }

    private void Player_OnStepEnd()
    {
        pathArrows[curentStep].gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !moved)
        {
            FindPath();
            ClearPathNodes();
        }
    }
    

    


    

    public void NewTurn()
    {
        //тут походу асинхронный метод, надо ждать выполенния
        BeforeNewTurn();
        MovePoints = BaseMovePoints;
    }


    

}
