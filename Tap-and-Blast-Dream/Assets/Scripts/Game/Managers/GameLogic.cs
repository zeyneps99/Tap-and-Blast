using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameLogic : Singleton<GameLogic>
{
    private LevelHelper _levelHelper;
    private FileHelper  _fileHelper;

    private void Awake() {
        _fileHelper = new FileHelper();
        GameObject go = new();
        go.name = "Level Helper";
        _levelHelper = go.AddComponent<LevelHelper>();
    }
    public void PrepareGame() {
        Level level = _fileHelper.GetCurrentLevel();
        _levelHelper.GenerateBoardForLevel(level);
    }

  
  



}
