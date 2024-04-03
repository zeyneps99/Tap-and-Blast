using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
[RequireComponent(typeof(Button))]
public class UIButton : MonoBehaviour, IAnimatable
{
    [SerializeField] private GameObject _foregroundObject;
    private Button _button;
    public static float scaleDuration = .2f; 
    private static float targetScale = 0.95f; 
    private static Ease easeType = Ease.OutQuad; 

    private List<UnityAction> _buttonCallbacks;

    private void Awake() {
        _button = GetComponent<Button>();
        _buttonCallbacks = new List<UnityAction>();
        _button.onClick.AddListener(OnClick);

    }


    private void OnDisable() {
        _button.onClick.RemoveListener(OnClick);
    }

    public void AddListener(UnityAction onClick) {
        if (_buttonCallbacks != null && !_buttonCallbacks.Contains(onClick)) {
            _buttonCallbacks.Add(onClick);
            _button.onClick.AddListener(onClick);
        }
    }

    public void RemoveListener(UnityAction onClick) {
    if (_buttonCallbacks != null && _buttonCallbacks.Count > 0 && _buttonCallbacks.Contains(onClick)) {
            _buttonCallbacks.Remove(onClick);
            _button.onClick.RemoveListener(onClick);
        }
    }

    private void OnClick()
    {
        Animate();
    }

    private IEnumerator AnimateThenExecuteCallbacks() {
           if (_buttonCallbacks != null && _buttonCallbacks.Count > 0) {
        _buttonCallbacks.ForEach(x => _button.onClick.RemoveListener(x));
    }

    Sequence sequence = DOTween.Sequence();

    if (_foregroundObject != null) {
        sequence.Append(_foregroundObject.transform.DOScale(targetScale, scaleDuration).SetEase(easeType));
        sequence.Append(_foregroundObject.transform.DOScale(1f, scaleDuration).SetEase(easeType));   
    }

    yield return sequence.AsyncWaitForCompletion();

    if (_buttonCallbacks != null && _buttonCallbacks.Count > 0) {
        _buttonCallbacks.ForEach(x => _button.onClick.AddListener(x));
    }

    sequence.Play();
}

    public void Animate(Action onComplete = null)
    {
        StartCoroutine(AnimateThenExecuteCallbacks());
    }
}
