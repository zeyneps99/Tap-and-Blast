using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private void Start() {
        if (!_isApplicationRunning) {
            StartMainMenu();
            _isApplicationRunning = true;
        }
    }

    private void OnEnable() {
        
        Events.GameEvents.OnPlay += PlayGame;
        Events.GameEvents.OnPlayerInput += RegisterBlastRequest;
        Events.GameEvents.OnBlast += PerformBlast;
    }



    private void OnDisable() {
        Events.GameEvents.OnPlay -= PlayGame;
        Events.GameEvents.OnPlayerInput -= RegisterBlastRequest;
        Events.GameEvents.OnBlast -= PerformBlast;
    }

    public bool IsInGame() {
        return _isInGame;
    }

    public void StartMainMenu() {
      _sceneManager.LoadScene((int) SceneTypes.MainScene, () => { Events.GameEvents.OnMainMenuStarted?.Invoke();});
    }

    private void PlayGame() {
        if (_isApplicationRunning && !_isInGame) {
            _isInGame = true;
             _sceneManager.LoadScene((int) SceneTypes.GameScene, () => 
             { 
                GameLogic.Instance.PrepareGame();
             });
        }
    }

    private void RegisterBlastRequest(Blastable blastable) {
        GameLogic.Instance.LookForBlast(blastable);
    }

    private void PerformBlast(List<Blastable> list)
    {
        GameLogic.Instance.HandleBlast(list);
        
    }
    

}
