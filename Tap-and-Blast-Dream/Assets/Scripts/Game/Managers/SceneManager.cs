using UnityEngine;


public class SceneManager : Singleton<SceneManager>
{
     public void LoadScene(int sceneType)
    {
        string sceneName = SceneTypeToSceneName(sceneType);
        if (!string.IsNullOrEmpty(sceneName))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
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
