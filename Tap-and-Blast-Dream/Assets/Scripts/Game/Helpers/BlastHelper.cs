using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastHelper : MonoBehaviour
{
    private Board _board;
    private List<Blastable> _blastList;
    private Blastable _chosenBlastable;
    private bool[,] _visited;

    public void Init(Board board) {
        _board = board;
    }

    public void LookForBlast(Blastable blastable) {
        _chosenBlastable = blastable;
        _visited = new bool[_board.Width, _board.Height];
        _blastList = new List<Blastable>();
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
    else if (_chosenBlastable.TryGetComponent(out TNT chosenTNT))
    {
        Debug.Log("blast tnt");
    }
    else
    {
    }
}


    private void PerformBlast()
    {
       Events.GameEvents.OnBlast?.Invoke(_blastList, _chosenBlastable);
    }

}





    

    

