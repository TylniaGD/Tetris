using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name != "UIScene" && currentScene.name != "GameScene")
        {
            Debug.Log("Scene doesn't exist in code!");
            return;
        }

        if (currentScene.name == "GameScene" && !IsSceneLoaded("UIScene"))
        {
            SceneManager.LoadScene("UIScene", LoadSceneMode.Additive);
        }
        else if (currentScene.name == "UIScene" && !IsSceneLoaded("GameScene"))
        {
            SceneManager.LoadScene("GameScene", LoadSceneMode.Additive);
        }
    }

    bool IsSceneLoaded(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name == sceneName)
                return true;
        }
        return false;
    }
}
