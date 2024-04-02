using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

[RequireComponent(typeof(Button))]
public class UIButton : MonoBehaviour
{
    [SerializeField] private GameObject _foregroundObject;
    private Button _button;
    public float scaleDuration = .2f; 
    public float targetScale = 0.95f; 
    public Ease easeType = Ease.OutQuad; 

    private void Awake() {
        _button = GetComponent<Button>();
    }

    private void OnEnable() {
        _button.onClick.AddListener(OnClickEffect);
    }

    private void OnDisable() {
        _button.onClick.RemoveListener(OnClickEffect);
    }

    private void OnClickEffect()
    {
        StartCoroutine(ScaleDown());
    }

    private IEnumerator ScaleDown() {
        Sequence sequence = DOTween.Sequence();

        if (_foregroundObject != null) {
            sequence.Append(_foregroundObject.transform.DOScale(targetScale, scaleDuration).SetEase(easeType));
            sequence.Append(_foregroundObject.transform.DOScale(1f, scaleDuration).SetEase(easeType));   
            sequence.Play();
        }
        
        yield return new WaitWhile(() => sequence.IsPlaying());
    }




    
}
