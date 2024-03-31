using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : BoardEntity, IAnimatable, IDamageable
{
    public ObstacleTypes Type {get; protected set;}

    public void TakeDamage()
    {
        throw new System.NotImplementedException();
    }

    public void SetType(int type) {
        Type = (ObstacleTypes) type;
    }

    public void Animate(Action onComplete = null)
    {
        throw new NotImplementedException();
    }
}
