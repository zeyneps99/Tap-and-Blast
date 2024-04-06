using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameLogic : Singleton<GameLogic>
{
    private LevelHelper _levelHelper;
    private FileHelper  _fileHelper;
    private BlastHelper _blastHelper;
    private void Awake() {
        _fileHelper = new FileHelper();
        GameObject go = new();
        go.name = "Helpers";
        _levelHelper = go.AddComponent<LevelHelper>();
        _blastHelper = go.AddComponent<BlastHelper>();
        
    }
    public void PrepareGame() {
        Level level = _fileHelper.GetCurrentLevel();
        _levelHelper.GenerateLevel(level);
        _blastHelper.Init(_levelHelper.GetBoard());
        Events.GameEvents.OnLevelGenerated?.Invoke(GetLevelInfo());
    }

    private LevelInfo GetLevelInfo() {
       return _levelHelper.GetLevelInfo();
    }

    public void LookForBlast(Blastable blastable) {
        if(_levelHelper)
        if (_blastHelper != null && blastable != null) {
            _blastHelper.LookForBlast(blastable);
        }
    }

    public void HandleBlast(List<Blastable> blastables, Blastable chosenBlastable) {
        if (blastables == null || blastables.Count <= 0 || !blastables.Contains(chosenBlastable)) {
            return;
        }
        else {
                _levelHelper.HandleBlast(blastables, chosenBlastable);
                if (_levelHelper.IsLevelOver()) {
                Events.GameEvents.OnGameOver?.Invoke(false);
            }
        }
    }

    public void EndGame(bool isWin) {
        _levelHelper.EndLevel();
        if (isWin) {
            int currentLevel = PlayerPrefs.GetInt("currentLevel" , 1);
            PlayerPrefs.SetInt("currentLevel", currentLevel + 1);
        } 
    }


}
