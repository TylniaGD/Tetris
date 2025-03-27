using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public GameStarter gameStarter;
    public GameObject pauseMenuPanel;

    [Space]

    public bool isGamePaused = false;

    void Start()
    {
        pauseMenuPanel.SetActive(false);
    }

    public void TogglePause()
    {
        if (gameStarter.gameIsStarted)
        {
            isGamePaused = !isGamePaused;
            pauseMenuPanel.SetActive(isGamePaused);

            Time.timeScale = isGamePaused ? 0f : 1f;
        }
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
        Debug.Log("Quit");
    }
}
