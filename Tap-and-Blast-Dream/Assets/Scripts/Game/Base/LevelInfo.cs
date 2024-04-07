using System.Collections.Generic;

public struct LevelInfo
{
    public Dictionary<ObstacleTypes, int> Goal;
    public int MoveCount { get; set; }

    public LevelInfo(Dictionary<ObstacleTypes, int> goal, int moveCount)
    {
        Goal = goal;
        MoveCount = moveCount;
    }
}
