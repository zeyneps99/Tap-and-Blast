

using System.Collections.Generic;
using UnityEngine;
using System;
public class FileHelper {

    private static string _path = "Levels/";
    private static string _filePrefix = "level_";

    public Level GetCurrentLevel() {
        Level level = new Level();
        int levelNum = PlayerPrefs.GetInt("currentLevel", 1);
        var file = Resources.Load<TextAsset>(_path + _filePrefix + (levelNum < 10 ? "0" : "") + levelNum);
        if (file != null) {
            level = JsonUtility.FromJson<Level>(file.text);
        }
        return level;
    }


}