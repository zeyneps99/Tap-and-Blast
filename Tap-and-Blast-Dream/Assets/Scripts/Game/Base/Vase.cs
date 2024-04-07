using System;
using DG.Tweening;
using UnityEngine;

public class Vase : Obstacle, IFallible
{
    public float Duration { get; set; }

   private void Start()
    {
        damage = 1f;
        health = 1f;
    }
    public void Fall(Vector2 newPos, Action onComplete = null)
    {
      transform.DOMoveY(newPos.y, Duration)
            .SetEase(Ease.OutBounce)
            .OnComplete(() =>
            {
                onComplete?.Invoke();
            });
    }
}
