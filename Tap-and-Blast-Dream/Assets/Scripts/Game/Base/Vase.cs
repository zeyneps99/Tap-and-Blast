using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vase : Obstacle, IFallible
{
    public float Duration { get; set; }

    public void Fall(Vector2 newPos, Action onComplete = null)
    {
        throw new NotImplementedException();
    }
}
