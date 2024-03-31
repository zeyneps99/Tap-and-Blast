using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimatable 
{
    public abstract void Animate(Action onComplete = null);
}
