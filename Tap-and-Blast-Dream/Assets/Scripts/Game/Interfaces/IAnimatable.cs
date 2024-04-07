using System;


public interface IAnimatable
{
    public abstract void Animate(Action onComplete = null);
}
