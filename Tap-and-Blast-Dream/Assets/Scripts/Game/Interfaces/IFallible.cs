using System;
using UnityEngine;

public interface IFallible
{
    public abstract void Fall(Vector2 newPos, Action onComplete = null);


}
