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
    private MainMenuUI _mainMenuUIPrefab;
    private MainMenuUI _mainMenuUI;
    private GameUI _gameUI;
    private GameUI _gameUIPrefab;
    private Canvas _canvas;

    void Awake()
    {
        DontDestroyOnLoad(this);
        GetPrefabs();
    }

    private void GetPrefabs()
    {
        _mainMenuUIPrefab = Resources.Load<MainMenuUI>(_resourcePath + "MainMenuUI");
        _gameUIPrefab = Resources.Load<GameUI>(_resourcePath + "GameUI");
    }

    private void OnEnable()
    {
        Events.GameEvents.OnMainMenuStarted += DisplayMainUI;
        Events.GameEvents.OnLevelGenerated += DisplayGameUI;
        Events.GameEvents.OnNotifyGameEnd += DisplayGameOver;
    }

    private void OnDisable()
    {
        Events.GameEvents.OnMainMenuStarted -= DisplayMainUI;
        Events.GameEvents.OnLevelGenerated -= DisplayGameUI;
        Events.GameEvents.OnNotifyGameEnd -= DisplayGameOver;
    }

    public void DisplayMainUI()
    {
        _canvas = FindObjectOfType<Canvas>();
        if (_gameUI != null)
        {
            _gameUI.gameObject.SetActive(false);
        }
        if (_mainMenuUIPrefab != null && _canvas != null)
        {
            if (_mainMenuUI == null)
            {
                _mainMenuUI = Instantiate(_mainMenuUIPrefab, _canvas.transform);
            }
        }

        _mainMenuUI.Init();

        if (_mainMenuUI != null && !_mainMenuUI.gameObject.activeInHierarchy)
        {
            _mainMenuUI.gameObject.SetActive(true);
        }


    }

    public void DisplayGameUI(LevelInfo info)
    {
        _canvas = FindObjectOfType<Canvas>();

        if (_mainMenuUI != null)
        {
            _mainMenuUI.gameObject.SetActive(false);
        }

        if (_gameUIPrefab != null && _canvas != null)
        {
            if (_gameUI == null)
            {
                _gameUI = Instantiate(_gameUIPrefab, _canvas.transform);

            }
        }

        if (_gameUI != null && !_gameUI.gameObject.activeInHierarchy)
        {
            _gameUI.gameObject.SetActive(true);
        }


        _gameUI.Init(info);
    }

    public void UpdateMoves(int moveCount)
    {
        if (_gameUI != null)
        {
            _gameUI.SetMoves(moveCount);
        }
    }

    public void UpdateGoal(Dictionary<ObstacleTypes, int> goal)
    {
        if (_gameUI != null)
        {
            _gameUI.SetGoal(goal);
        }
    }

    public void DisplayGameOver(bool isWin)
    {
        if (!isWin)
        {
            ShowFail();
        }
        else
        {
            ShowVictory();
            
        }
    }

    private void ShowVictory()
    {
        if (_gameUI != null)
        {
            _gameUI.DisplayCelebrationPanel();
        }
    }
    private void ShowFail()
    {
        if (_gameUI != null)
        {
            _gameUI.DisplayFailPopUp(true);
        }
    }


}
