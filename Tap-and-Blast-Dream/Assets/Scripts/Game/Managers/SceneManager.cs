using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : Singleton<SceneManager>
{
     public void LoadScene(int sceneType, Action onComplete = null)
    {
       string sceneName = SceneTypeToSceneName(sceneType);
        if (!string.IsNullOrEmpty(sceneName))
        {
            if (onComplete != null) {
                UnityEngine.SceneManagement.SceneManager.sceneLoaded += (scene, mode) => {
                    onComplete.Invoke(); 
            };
            }
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single); 
        }
        else
        {
            Debug.LogError("Scene name is not defined for the given SceneType.");
        }
    }

    private string SceneTypeToSceneName(int sceneType)
    {
       return ((SceneTypes)sceneType).ToString();  
    }


}
