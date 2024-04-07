using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private bool _isApplicationRunning;
    private bool _isInGame = false;

    private SceneManager _sceneManager;

    void Awake()
    {
        DontDestroyOnLoad(this);
        _sceneManager = SceneManager.Instance;
    }

    private void Start()
    {
        if (!_isApplicationRunning)
        {
            _isApplicationRunning = true;
            StartMainMenu();
        }
    }

    private void OnEnable()
    {

        Events.GameEvents.OnPlay += PlayGame;
        Events.GameEvents.OnPlayerInput += RegisterBlastRequest;
        Events.GameEvents.OnBlast += PerformBlast;
        Events.GameEvents.OnGameOver += EndGame;
        Events.GameEvents.OnTryAgain += TryAgain;
        Events.GameEvents.OnQuitLevel += OnQuitLevel;
    }

    private void OnDisable()
    {
        Events.GameEvents.OnPlay -= PlayGame;
        Events.GameEvents.OnPlayerInput -= RegisterBlastRequest;
        Events.GameEvents.OnBlast -= PerformBlast;
        Events.GameEvents.OnGameOver -= EndGame;
        Events.GameEvents.OnTryAgain -= TryAgain;
        Events.GameEvents.OnQuitLevel -= OnQuitLevel;
    }

    public bool IsInGame()
    {
        return _isInGame;
    }

    public void TryAgain()
    {
        if (!IsInGame() && _isApplicationRunning)
        {
            _isInGame = true;
            Debug.Log("tryagain");
            GameLogic.Instance.PrepareGame();

        }
    }

    private void OnQuitLevel()
    {

        StartMainMenu();

    }
    public void StartMainMenu()
    {
        if (!IsInGame() && _isApplicationRunning)
        {
            _sceneManager.LoadScene((int)SceneTypes.MainScene, () => { Events.GameEvents.OnMainMenuStarted?.Invoke(); });

        }
    }

    private void PlayGame()
    {
        if (_isApplicationRunning && !_isInGame)
        {
            _isInGame = true;
            _sceneManager.LoadScene((int)SceneTypes.GameScene, () =>
            {
                GameLogic.Instance.PrepareGame();
            });
        }
    }
    private void RegisterBlastRequest(Blastable blastable)
    {
        GameLogic.Instance.LookForBlast(blastable);
    }

    private void PerformBlast(List<BoardEntity> list, Blastable chosenBlastable)
    {
        GameLogic.Instance.HandleBlast(list, chosenBlastable);
    }

    private void EndGame(bool isWin)
    {
        _isInGame = false;

        GameLogic.Instance.EndGame(isWin);
        Events.GameEvents.OnNotifyGameEnd?.Invoke(isWin);

        if (isWin)
        {
            StartCoroutine(ReturnToMainMenu());

        }

    }


    private IEnumerator ReturnToMainMenu()
    {
        yield return new WaitForSeconds(5f);
        StartMainMenu();

    }

}
