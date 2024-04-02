using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Image))]
public class Cube : Blastable, IAnimatable, IFallible
{
    public CubeTypes Type { private set; get; }
    public float Duration { get; set; }

    private Image _image;

    [SerializeField] private ParticleSystem _blastParticles;

    private static string _spritePath = "Sprites/Cube/DefaultState/";
    private static string _matPath = "Materials/";

    public void SetType(int cubeType)
    {
        Type = (CubeTypes)cubeType;
        SetSprite(cubeType);
        SetParticleMaterial(cubeType);
        Duration = .3f;
    }

    private void SetSprite(int type)
    {
        if ((CubeTypes)type != CubeTypes.None)
        {
            Sprite sprite = Resources.Load<Sprite>(_spritePath + GetSpriteName(type));
            _image = GetComponent<Image>();
            if (sprite != null && _image != null)
            {
                _image.sprite = sprite;
                _image.enabled = true;
            }
        }
    }

    private void SetParticleMaterial(int type)
    {
        if ((CubeTypes)type != CubeTypes.None)
        {
            Material mat = Resources.Load<Material>(_matPath + GetSpriteName(type) + "Particles");
            if (_blastParticles != null && mat != null)
            {
                if (_blastParticles.TryGetComponent(out ParticleSystemRenderer renderer))
                {
                    renderer.material = mat;
                }
            }
        }
    }

    private string GetSpriteName(int type)
    {
        switch ((CubeTypes)type)
        {
            case CubeTypes.Yellow:
                return "yellow";
            case CubeTypes.Red:
                return "red";
            case CubeTypes.Blue:
                return "blue";
            case CubeTypes.Green:
                return "green";
            default:
                return "yellow";
        }
    }

    public override bool CanBlastNeighbor(Blastable neighbor)
    {
        return (neighbor.TryGetComponent(out Cube cube) && cube.Type == Type);
    }

    public void Animate(Action onComplete)
    {
        // gameObject.SetActive(false);
        if (_blastParticles != null)
        {
            _image.enabled = false;
            StartCoroutine(ParticleAnimationRoutine(_blastParticles, onComplete));
        }
    }

    private IEnumerator ParticleAnimationRoutine(ParticleSystem particles, Action onComplete = null)
    {
        particles.Play();
        yield return new WaitUntil(() => !particles.isPlaying);
        onComplete?.Invoke();
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
