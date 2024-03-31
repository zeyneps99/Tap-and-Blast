using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Cube : Blastable, IAnimatable, IFallible
{
    public CubeTypes Type {private set; get;}
    private Image _image; 

    [SerializeField] private ParticleSystem _blastParticles;

    private static string _spritePath = "Sprites/Cube/DefaultState/";
    private static string _matPath = "Materials/";
    
    public void SetType(int cubeType) {
        Type = (CubeTypes) cubeType;
        SetSprite(cubeType);
        SetParticleMaterial(cubeType);
    }

    private void SetSprite(int type) {
        if ((CubeTypes) type!= CubeTypes.None) {
            Sprite sprite = Resources.Load<Sprite>(_spritePath + GetSpriteName(type));
            _image = GetComponent<Image>();
            if (sprite != null && _image != null) {
                _image.sprite = sprite;
                _image.enabled = true;
            }
        }
    }

    private void SetParticleMaterial(int type) {
        if ((CubeTypes) type!= CubeTypes.None) {
            Material mat = Resources.Load<Material>(_matPath + GetSpriteName(type) + "Particles");
            if (_blastParticles != null && mat != null) {
                if (_blastParticles.TryGetComponent(out ParticleSystemRenderer renderer)) {
                    renderer.material = mat;
                }
            }
        }
    }

    private string GetSpriteName(int type) {
        switch((CubeTypes) type) {
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

    public void Fall()
    {
        throw new System.NotImplementedException();
    }

    public override bool CanBlastNeighbor(Blastable neighbor)
    {
        return (neighbor.TryGetComponent(out Cube cube) && cube.Type == Type);
    }

    public void Animate()
    {
       // gameObject.SetActive(false);
        if(_blastParticles != null) {
            _image.enabled = false;
            StartCoroutine(ParticleAnimationRoutine(_blastParticles));
        }
    }

    private IEnumerator ParticleAnimationRoutine(ParticleSystem particles) {
        particles.Play();
         while (particles.isPlaying)
        {
            yield return null; 
        }
    }
}
