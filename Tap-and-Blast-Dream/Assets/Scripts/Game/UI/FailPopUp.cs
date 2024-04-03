using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FailPopUp : MonoBehaviour
{
    [SerializeField] private UIButton _tryAgainButton;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private Button _closeButton;
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
        gameObject.SetActive(isShow);
        Events.GameEvents.OnQuitLevel?.Invoke();
    }

}
