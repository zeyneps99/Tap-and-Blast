using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardEntity : Entity, IClearable
{
    public Vector2Int Position = Vector2Int.down;
    public void Clear()
    {
    }
}
