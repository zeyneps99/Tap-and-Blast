using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FailPopUp : MonoBehaviour, IAnimatable
{
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private Button _closeButton;

    [SerializeField] private Transform _onPosition;
    [SerializeField] private Transform _offPosition;

    [SerializeField] private GameObject _panel;

    [SerializeField] private UIButton _tryAgainButton;

    private static string _levelstring = "Level ";

    private static float _animationDuration = .5f;
    private static Ease _ease = Ease.OutBack;
    private Vector3 _destinationPosition;


    private void OnDisable()
    {
        _tryAgainButton?.RemoveListener(TryAgainClicked);
        _closeButton?.onClick.RemoveListener(ClosePopup);
    }

    private void OnEnable()
    {
        _tryAgainButton?.AddListener(TryAgainClicked);
        _closeButton?.onClick.AddListener(ClosePopup);
    }

    public void Init()
    {
        if (_levelText != null)
        {
            _levelText.text = _levelstring + PlayerPrefs.GetInt("currentLevel", 1);
        }
    }

    private void ClosePopup()
    {
        Display(false);
        Animate(() =>
        {
            Events.GameEvents.OnQuitLevel?.Invoke();
            gameObject.SetActive(false);
        });
    }

    private void TryAgainClicked()
    {
        Display(false);
        Animate(() =>
        {
            gameObject.SetActive(false);
            Events.GameEvents.OnTryAgain?.Invoke();
        });

    }

    public void Display(bool isShow)
    {
        if (_onPosition != null && _offPosition != null && _panel != null)
        {
            _panel.transform.position = isShow ? _offPosition.position : _onPosition.position;
            _destinationPosition = isShow ? _onPosition.position : _offPosition.position;
        }
    }

    public void Animate(Action onComplete = null)
    {
        StartCoroutine(AnimateBounce(onComplete));
    }

    private IEnumerator AnimateBounce(Action onComplete = null)
    {
        Sequence sequence = DOTween.Sequence();
        if (_panel != null)
        {
            sequence.Append(_panel.transform.DOMoveY(_destinationPosition.y, _animationDuration)
                    .SetEase(_ease));

            sequence.OnComplete(() =>
            {
                onComplete?.Invoke();
            });
        }
        sequence.Play();
        yield return sequence.AsyncWaitForCompletion();
    }
}
