using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : Singleton<GameLogic>
{
    private UIManager _uiManager;
    private SceneManager _sceneManager;

    private FileHelper  _fileHelper;
    private LevelHelper _levelHelper;

    private void Awake() {
        _uiManager = UIManager.Instance;
        _sceneManager = SceneManager.Instance;

        _levelHelper = new LevelHelper();
        _fileHelper = new FileHelper();
    }


    public void StartMainScene() {
        _sceneManager.LoadScene((int)SceneTypes.MainScene);
    }

    public void StartGame() {
        Level level = _fileHelper.GetCurrentLevel();
        _levelHelper.SetLevel(level);
        
        _sceneManager.LoadScene((int) SceneTypes.GameScene);
    }




}
