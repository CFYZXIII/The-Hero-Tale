using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;
using System.Linq;

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
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !moved)
        {
            FindPath();
            ClearPathNodes();
        }
    }
    private bool moved;
    private IEnumerator Move(int i)
    {
        yield return null;
        if (i < pathNodes.Count && MovePoints > 0)
        {
            moved = true;
            while (((Vector2)transform.position - pathNodes[i].transformPos).magnitude > 0.001f)
            {
                transform.position = Vector2.MoveTowards(transform.position, pathNodes[i].transformPos, 0.01f);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, pathNodes[i].algle), 0.1f);
                yield return new WaitForSeconds(0.01f);
            }
            pathArrows[i].gameObject.SetActive(false);
            i++;
            MovePoints--;


            StartCoroutine(Move(i));
        }
        else
            moved = false;

    }

    public void GoMove()
    {
        StartCoroutine(Move(0));
    }

    public void NewTurn()
    {
        MovePoints = BaseMovePoints;
    }




}
