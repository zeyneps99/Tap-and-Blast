using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct LevelInfo 
{
    public Goal Goal {get; set;}
    public int MoveCount {get; set;}

    public LevelInfo(Goal goal, int moveCount) {
        Goal = goal;
        MoveCount = moveCount;
    }
}
