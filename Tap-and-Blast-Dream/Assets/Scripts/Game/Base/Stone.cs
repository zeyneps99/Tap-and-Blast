using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : Obstacle
{
    private void Awake() {
        Type = ObstacleTypes.Stone;
    }
}
