using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private GameStateManager _gameStateManager;
    private bool _isApplicationRunning;
    private bool _isInGame = false;
    void Awake()
    {
        DontDestroyOnLoad(this);
         _gameStateManager = new GameStateManager();
    }

    private void Start() {
        if (!_isApplicationRunning) {
            _gameStateManager.SwitchState(new IdleState());
            _isApplicationRunning = true;
        }
    }

    private void OnEnable() {
        
        Events.GameEvents.OnPlay += StartGame;
    }

    private void OnDisable() {
        Events.GameEvents.OnPlay -= StartGame;
    }

    private void StartGame() {
        if (_isApplicationRunning) {
            _gameStateManager.SwitchState(new InGameState());
            _isInGame = true;
        }
    }



}
