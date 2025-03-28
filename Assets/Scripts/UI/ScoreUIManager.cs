using UnityEngine;
using TMPro;

public class ScoreUIManager : MonoBehaviour
{
    TextMeshProUGUI scoreText;

    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateScoreUI(Player player, int newScore)
    {
        if (player.name + "UI" != LayerMask.LayerToName(gameObject.layer)) return; // Preventing duplicate execution of the method

        scoreText.text = "Score: " + newScore;
    }

    void OnEnable()
    {
        TetrisGameManager.OnScoreUpdated += UpdateScoreUI;
    }

    void OnDisable()
    {
        TetrisGameManager.OnScoreUpdated -= UpdateScoreUI;
    }
}