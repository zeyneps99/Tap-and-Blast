using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FailPopUp : MonoBehaviour, IAnimatable
{
    [SerializeField] private UIButton _tryAgainButton;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private Button _closeButton;

    [SerializeField] private Transform _onPosition;
    [SerializeField] private Transform _offPosition;

    [SerializeField] private GameObject _panel;

    private static string _levelstring = "Level ";

    private void OnEnable() {
        _tryAgainButton?.AddListener(TryAgainClicked);
        _closeButton?.onClick.AddListener(ClosePopup);
    }
    private void OnDisable()
    {
        _closeButton?.onClick.RemoveListener(ClosePopup);
        _tryAgainButton?.RemoveListener(TryAgainClicked);
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
    }

    private void TryAgainClicked()
    {   
        Display(false);
    }

    //TODO: add animation
    public void Display(bool isShow)
    {
        if (_onPosition != null && _offPosition != null && _panel != null) {
        _panel.transform.position = isShow ? _onPosition.position : _offPosition.position;
        gameObject.SetActive(isShow);
        }
        Events.GameEvents.OnQuitLevel?.Invoke();
    }

    //TODO: animate bounce in 
    public void Animate(Action onComplete = null)
    {

    }
}
