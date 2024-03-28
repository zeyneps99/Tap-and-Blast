using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : Singleton<GameLogic>
{
    private UIManager _uiManager;
    private SceneManager _sceneManager;


    private void Awake() {
        _uiManager = UIManager.Instance;
        _sceneManager = SceneManager.Instance;
    }


    public void StartMainScene() {
        _sceneManager.LoadScene((int)SceneTypes.MainScene);
    }

    public void StartGame() {
        _sceneManager.LoadScene((int) SceneTypes.GameScene);
    }


    

}
