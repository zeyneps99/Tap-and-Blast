using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vase : Obstacle, IFallible
{
    private void Awake() {
        Type = ObstacleTypes.Vase;
    }
    public void Fall()
    {
        throw new System.NotImplementedException();
    }
}
