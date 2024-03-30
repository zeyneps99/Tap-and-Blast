using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    private static string _resourcePath = "Prefabs/UI/";
    private MainMenuUI _mainMenuUI;
    private GameUI _gameUI;
    private Canvas _canvas;

    void Awake()
    {
        DontDestroyOnLoad(this);
        GetPrefabs();
    }
 
    private void GetPrefabs() {
        _mainMenuUI = Resources.Load<MainMenuUI>(_resourcePath + "MainMenuUI");
        _gameUI = Resources.Load<GameUI>(_resourcePath + "GameUI");
    }

    private void OnEnable() {
        Events.GameEvents.OnMainMenuStarted += DisplayMainUI;
        Events.GameEvents.OnLevelGenerated += DisplayGameUI;
    }

    private void OnDisable() {
        Events.GameEvents.OnMainMenuStarted -= DisplayMainUI;
        Events.GameEvents.OnLevelGenerated -= DisplayGameUI;
    }
    
    public void DisplayMainUI() {
        _canvas = FindObjectOfType<Canvas>();
        if (_mainMenuUI != null && _canvas != null) {
            _mainMenuUI = Instantiate(_mainMenuUI, _canvas.transform);
            _mainMenuUI.Init();
        }
    }

    public void DisplayGameUI(LevelInfo info) {
        _canvas = FindObjectOfType<Canvas>();
        if (_gameUI != null && _canvas != null) {
            _gameUI = Instantiate(_gameUI, _canvas.transform);
            _gameUI.Init(info);
        }
    }

}
