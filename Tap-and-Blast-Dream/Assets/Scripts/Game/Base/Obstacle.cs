using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : BoardEntity, IAnimatable, IDamageable
{
    public ObstacleTypes Type { get; protected set; }
    protected float health;

    public float Health
    {
        get { return health; }
        set { health = Mathf.Max(value, 0f); }
    }

    protected float damage = 1f; 

    public float Damage
    {
        get { return damage; }
        set { damage = Mathf.Max(value, 0f); } 
    }
    public void TakeDamage()
    {
        Health -= Damage;

        if (Health <= 0) {
            Debug.Log("destroy " + name);
        }
    }

    public void SetType(int type)
    {
        Type = (ObstacleTypes)type;
    }

    public void Animate(Action onComplete = null)
    {
        // throw new NotImplementedException();
    }




}
