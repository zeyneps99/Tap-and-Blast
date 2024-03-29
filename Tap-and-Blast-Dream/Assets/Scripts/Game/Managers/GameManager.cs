using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private GameStateManager _gameStateManager;
    private bool _isApplicationRunning;
    private bool _isInGame = false;

    private UIManager _uiManager;
    private SceneManager _sceneManager;

    void Awake()
    {
        DontDestroyOnLoad(this);
        _gameStateManager = new GameStateManager();
        _uiManager = UIManager.Instance;
        _sceneManager = SceneManager.Instance;

    }

    private void Start() {
        if (!_isApplicationRunning) {
            _gameStateManager.SwitchState(new IdleState());
            _isApplicationRunning = true;
        }
    }

    private void OnEnable() {
        
        Events.GameEvents.OnPlay += PlayClicked;
    }

    private void OnDisable() {
        Events.GameEvents.OnPlay -= PlayClicked;
    }

    public bool IsInGame() {
        return _isInGame;
    }

    public void StartMainMenu() {
      _sceneManager.LoadScene((int) SceneTypes.MainScene, () => { _uiManager.DisplayUI((int) SceneTypes.MainScene);});
    }

    private void PlayClicked() {
        if (_isApplicationRunning && !_isInGame) {
            _isInGame = true;
            _gameStateManager.SwitchState(new InGameState());
        }
    }

    public void StartGame() {

        if(_isApplicationRunning && _isInGame) {
            _sceneManager.LoadScene((int) SceneTypes.GameScene, () => 
            { 
                GameLogic.Instance.PrepareGame();
                _uiManager.DisplayUI((int) SceneTypes.GameScene);});

        }
    }


}
