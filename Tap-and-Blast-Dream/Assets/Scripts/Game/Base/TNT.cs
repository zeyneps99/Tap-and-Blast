using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Image))]

public class TNT : Blastable, IAnimatable, IFallible
{
    public float Duration { get; set; }
    private float rotationAngle = 360f;

    [SerializeField] private ParticleSystem _blastParticles;

    private Image _image;


    private void Start()
    {
        Duration = .3f;
        _image = GetComponent<Image>();


    }
    public override bool CanBlastNeighbor(Blastable neighbor)
    {
        return (neighbor.TryGetComponent(out TNT _) || neighbor.TryGetComponent(out Cube _));
    }
    public void Animate(Action onComplete = null)
    {
        if (_blastParticles != null)
        {
            _image.enabled = false;

            Sequence sequence = DOTween.Sequence();
            StartCoroutine(ParticleAnimationRoutine(_blastParticles, onComplete));
            sequence.Append(transform.DOShakeScale(Duration)).SetEase(Ease.OutBounce)
            .OnComplete(() =>
            {

            });

        }
        else
        {
            onComplete?.Invoke();
        }
    }


    private IEnumerator ParticleAnimationRoutine(ParticleSystem particles, Action onComplete = null)
    {
        particles.Play();
        yield return new WaitUntil(() => !particles.isPlaying);
        onComplete?.Invoke();
    }

    //TODO: fix animation
    public void Fall(Vector2 newPos, Action onComplete = null)
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(transform.DORotate(new Vector3(0, rotationAngle, 0), Duration, RotateMode.FastBeyond360));

        sequence.Join(transform.DOMoveY(newPos.y, Duration)
            .SetEase(Ease.OutBounce));

        sequence.OnComplete(() =>
        {
            onComplete?.Invoke();
        });

        sequence.Play();


    }


}
