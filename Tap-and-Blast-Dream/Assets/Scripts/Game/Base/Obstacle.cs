using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Obstacle : BoardEntity, IAnimatable, IDamageable
{
    public ObstacleTypes Type { get; protected set; }
    protected float health;

    [SerializeField] protected ParticleSystem blastParticles;

    private Image _image;

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
    public bool TakeDamage()
    {
        Health -= Damage;
        if (Health <= 0)
        {
            return true;
        }
        return false;
    }

    public void SetType(int type)
    {
        Type = (ObstacleTypes)type;
        _image = GetComponent<Image>();

        if (_image != null)
        {
            _image.enabled = true;
        }
    }

    public void Animate(Action onComplete)
    {
        if (blastParticles != null)
        {
            if (_image != null)
            {
                _image.enabled = false;
            }
            StartCoroutine(ParticleAnimationRoutine(blastParticles, onComplete));
        }
    }

    private IEnumerator ParticleAnimationRoutine(ParticleSystem particles, Action onComplete = null)
    {
        particles.Play();
        yield return new WaitUntil(() => !particles.isPlaying);
        onComplete?.Invoke();
    }





}
