using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastHelper : MonoBehaviour
{
    private Board _board;
    private List<BoardEntity> _blastList;
    private Blastable _chosenBlastable;
    private bool[,] _visited;

    public void Init(Board board)
    {
        _board = board;
    }

    public void LookForBlast(Blastable blastable)
    {
        _chosenBlastable = blastable;
        _visited = new bool[_board.Width, _board.Height];
        _blastList = new List<BoardEntity>();
        Vector2Int pos = _board.GetPositionOfItem(_chosenBlastable);
        DFS(pos.x, pos.y);

        if (_blastList.Count >= 2)
        {
            PerformBlast();
        }
    }


    private void DFS(int row, int column)
    {
        if (row < 0 || row >= _board.Width || column < 0 || column >= _board.Height || _visited[row, column])
        {
            return;
        }

        _visited[row, column] = true;
        BoardEntity neighbor = _board.Items[row, column];

        if (neighbor == null)
        {
            return;
        }

        if (_chosenBlastable.TryGetComponent(out Cube chosenCube))
        {
            if (neighbor.TryGetComponent(out Cube neighborCube) && chosenCube.Type == neighborCube.Type)
            {
                _blastList.Add(neighborCube);

                DFS(row - 1, column);
                DFS(row + 1, column);
                DFS(row, column - 1);
                DFS(row, column + 1);
            }
        }
        else if (_chosenBlastable.TryGetComponent(out TNT _))
        {
            for (int i = -2; i <= 2; i++)
            {
                for (int j = -2; j <= 2; j++)
                {
                    int newRow = row + i;
                    int newColumn = column + j;

                    if (Mathf.Abs(i) <= 2 && Mathf.Abs(j) <= 2 && newRow >= 0 && newRow < _board.Width && newColumn >= 0 && newColumn < _board.Height)
                    {
                        BoardEntity neighborEntity = _board.Items[newRow, newColumn];
                        if (neighborEntity != null && !neighborEntity.TryGetComponent(out Obstacle _))
                        {
                            _blastList.Add(neighborEntity);
                        }
                    }
                }
            }
        }
    }



    private void PerformBlast()
    {
        Events.GameEvents.OnBlast?.Invoke(_blastList, _chosenBlastable);
    }

}









