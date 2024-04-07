using System;
using UnityEngine;

public static class Utils
{
    public static SceneTypes GetSceneType(string sceneName)
    {
        SceneTypes type = SceneTypes.None;
        if (string.IsNullOrEmpty(sceneName))
        {
            return type;
        }
        else
        {
            try
            {
                type = (SceneTypes)Enum.Parse(typeof(SceneTypes), sceneName);
            }
            catch
            {
                Debug.LogError("Invalid scene");
            }
            return type;
        }
    }

}
