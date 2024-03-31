using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardEntity : Entity, IClearable
{
    private Vector2Int _position = Vector2Int.down;
    private Board _board;
    public virtual void Clear()
    {
        Set(Vector2Int.down, null);
    }

    public void Set(Vector2Int pos, Board board) {
        _board = board;
        _position = pos;
    }

    public Vector2Int GetPosition() {
        return _position;
    }

    public Board GetBoard() {
        return _board;
    }


}
