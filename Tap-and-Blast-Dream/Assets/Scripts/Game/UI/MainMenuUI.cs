using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private TextMeshProUGUI _text;

    private static string _levelstring = "Level ";
    private static string _completeString = "Complete";

    private int _currentLevel = 1;

    private void OnEnable() {
        
    }

    private void OnDisable() {
        
    }

    public void Init() {

        if (_playButton != null) {
            
            _currentLevel = PlayerPrefs.GetInt("currentLevel", 1);

            if (_text != null) {

                _text.text = _levelstring + _currentLevel;

            }
            _playButton.onClick.AddListener(Play);

        }

    }

    private void Play() {
        Events.GameEvents.OnPlay?.Invoke();
    }
    
}
