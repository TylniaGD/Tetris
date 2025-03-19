using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [SerializeField] GameObject startButton;
    public bool gameIsStarted = false;

    void Start()
    {
        startButton.SetActive(true);
    }

    public void StartGame()
    {
        if (!gameIsStarted)
        {
            gameIsStarted = true;
            Debug.Log("Game started");
            Destroy(startButton);
        }
    }
}