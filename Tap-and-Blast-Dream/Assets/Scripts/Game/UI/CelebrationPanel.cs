using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CelebrationPanel : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _celebrationText;
    [SerializeField] private GameObject _starObject;

    [SerializeField] private GameObject _glowObject;

    [SerializeField] private ParticleSystem _starParticles;

    [SerializeField] private ParticleSystem _fireworkParticles;

    private static string _celebrationString = "Perfect!";
    public float _letterAnimationDelay = .1f;

    private float _starAnimationDuration = 1.2f;


    public void Init(){
        if (_celebrationText != null)
        {
            _celebrationText.text = _celebrationString;
            _celebrationText.maxVisibleCharacters = 0;
        }

        if (_glowObject != null)
        {
            _glowObject.SetActive(false);
        }

        if (_starParticles != null)
        {
            _starParticles.Stop();
        }

        if (_fireworkParticles != null)
        {
            _fireworkParticles.Stop();
        }
        if (_starObject != null) {
        _starObject.transform.localScale = 8 * Vector3.one;
        _starObject.SetActive(false);
        }
       
    }

    public void Display(){
        
        AnimateText();

    }

    private void AnimateText()
    {
        int totalCharacters = _celebrationText.text.Length;

        StartCoroutine(RevealText(totalCharacters));


    }

    private void AnimateStar()
    {
        if (_starObject != null)
        {

            DG.Tweening.Sequence sequence = DOTween.Sequence();
            _starObject.SetActive(true);
            sequence.Append(_starObject.transform.DOScale(Vector3.one, .5f)).SetEase(Ease.OutCubic);
            sequence.Join(_starObject.transform.DORotate(new Vector3(0, 0, 180), 1f));
            sequence.Play().WaitForCompletion();
            _glowObject?.SetActive(true);
            _starParticles?.Play();
            _fireworkParticles?.Play();

        }
    }

    private IEnumerator RevealText(int totalCharacters)
    {
        for (int i = 0; i <= totalCharacters; i++)
        {
            _celebrationText.maxVisibleCharacters = i;
            DG.Tweening.Sequence sequence = DOTween.Sequence();
            sequence.Append(_celebrationText.transform.DOScale(1.5f, _letterAnimationDelay * 2)
            .SetEase(Ease.OutBack));
            sequence.Append(_celebrationText.transform.DOScale(1f, _letterAnimationDelay * 2)
            .SetEase(Ease.OutBack));
            sequence.Play();
           yield return null;
        }
        AnimateStar();

    }
}

