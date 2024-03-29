

using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;
public class FileHelper {

    private static string _path = "Levels/";
    private static string _filePrefix = "level_";

    public Level GetCurrentLevel() {
        Level level = new Level();
        int levelNum = PlayerPrefs.GetInt("currentLevel", 1);
        Debug.Log(levelNum);
        var file = Resources.Load<TextAsset>(_path + _filePrefix + (levelNum < 10 ? "0" : "") + levelNum);
        if (file != null) {
            level = JsonConvert.DeserializeObject<Level>(file.text);
        }
        return level;
    }


}