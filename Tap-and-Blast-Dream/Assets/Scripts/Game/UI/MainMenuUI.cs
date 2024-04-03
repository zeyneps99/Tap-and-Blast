using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private UIButton _playButton;
    [SerializeField] private TextMeshProUGUI _text;

    private static string _levelstring = "Level ";
    private static string _completeString = "Complete";


    public void Init()
    {

        if (_text != null)
        {
            _text.text = _levelstring + PlayerPrefs.GetInt("currentLevel", 1);
        }
        
        if (_playButton != null) {
            _playButton.AddListener(Play);
        }
    }

    private void OnDisable()
    {
        if (_playButton != null) {
            _playButton.RemoveListener(Play);
        }
    }

    private void Play()
    {
        Events.GameEvents.OnPlay?.Invoke();
    }

}
