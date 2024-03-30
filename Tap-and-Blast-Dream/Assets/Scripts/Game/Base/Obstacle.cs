using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : BoardEntity, IAnimatable, IDamageable
{
    public ObstacleTypes Type {get; protected set;}
    public void Animate()
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage()
    {
        throw new System.NotImplementedException();
    }
}
