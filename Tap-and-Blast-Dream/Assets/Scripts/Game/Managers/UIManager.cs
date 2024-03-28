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
    private Canvas _canvas;
    void Awake()
    {
        DontDestroyOnLoad(this);
        GetPrefabs();
    }
    private void OnEnable() {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += HandleSceneChange;
    }

    private void OnDisable() {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= HandleSceneChange;
    }

    private void HandleSceneChange(Scene scene, LoadSceneMode _)
    {
        _canvas = FindObjectOfType<Canvas>();
        SceneTypes type = Utils.GetSceneType(scene.name);
        switch(type) {
            case SceneTypes.MainScene:
            DisplayMainUI();
            break;

            default:
            break;
        }
    }

    private void GetPrefabs() {
        _mainMenuUI = Resources.Load<MainMenuUI>(_resourcePath + "MainMenuUI");
    }

    private void DisplayMainUI() {
        if (_mainMenuUI != null && _canvas != null) {
            _mainMenuUI = Instantiate(_mainMenuUI, _canvas.transform);
        }
    }

}
