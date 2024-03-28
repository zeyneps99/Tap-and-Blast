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
        int sceneType = (int) Utils.GetSceneType(scene.name);
        DisplayUI(sceneType);
    }

    private void GetPrefabs() {
        _mainMenuUI = Resources.Load<MainMenuUI>(_resourcePath + "MainMenuUI");
    }

    
    public void DisplayUI(int scene) {

        _canvas = FindObjectOfType<Canvas>();

        SceneTypes type = (SceneTypes) scene;

        switch(type) {
            case SceneTypes.MainScene:
            DisplayMainUI();
            break;

            default:
            break;
        }
    }

    private void DisplayMainUI() {

        if (_mainMenuUI != null && _canvas != null) {
            _mainMenuUI = Instantiate(_mainMenuUI, _canvas.transform);
            _mainMenuUI.Init();
        }
    }

}
