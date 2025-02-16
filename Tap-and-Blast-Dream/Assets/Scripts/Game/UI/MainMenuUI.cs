
using TMPro;
using UnityEngine;

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
            int currentLevel = PlayerPrefs.GetInt("currentLevel", 1);
   
            _text.text = currentLevel <= 10 ? _levelstring + currentLevel.ToString() : _completeString;
        }

        if (_playButton != null)
        {
            _playButton.AddListener(Play);
        }
    }

    private void OnDestroy()
    {
        if (_playButton != null)
        {
            _playButton.RemoveListener(Play);
        }
    }

    private void Play()
    {
        Events.GameEvents.OnPlay?.Invoke();
    }

}
