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
        DFS(_chosenBlastable.GetPosition().x, _chosenBlastable.GetPosition().y);
        //TODO: change for TNT checks
        if (_blastList.Count >= 2)
        {
            PerformMatch();
        }
    }

     private void DFS(int row, int column)
     {
        if (_board == null) {
            return;
        }
        
        if (row < 0 || row >= _board.Width|| column < 0 || column >= _board.Height)
        {
            return;
        }
        if (_visited[row, column]) {
            return;
        }
        BoardEntity neighbor = _board.Items[row, column];
        if (neighbor == null) {
            return;
        } else {
            if (neighbor.TryGetComponent(out Blastable blastableNeighbor)) {
                
               if( _chosenBlastable.CanBlastNeighbor(blastableNeighbor)) {
                _visited[row, column] = true;
                _blastList.Add(blastableNeighbor);

                DFS(row - 1, column);
                DFS(row + 1, column);
                DFS(row, column - 1);
                DFS(row, column + 1);
               }
               else {

                return;
               }
            } else {
                return;
            }
        }
     }

    private void PerformMatch()
    {
       Events.GameEvents.OnBlast?.Invoke(_blastList);
    }
}





    

    

