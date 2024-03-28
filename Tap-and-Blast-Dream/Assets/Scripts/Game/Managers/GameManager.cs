using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private GameStateManager _gameStateManager;
    private bool _isApplicationRunning;
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




}
