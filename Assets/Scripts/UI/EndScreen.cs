using System.Collections;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] Player player1;
    [SerializeField] Player player2;

    [SerializeField] GameObject player1EndScreen;
    [SerializeField] GameObject player2EndScreen;

    void Start()
    {
        StartCoroutine(FindPlayers());

        player1EndScreen.SetActive(false);
        player2EndScreen.SetActive(false);
    }

    void Update()
    {
        if (player1EndScreen != null && player2EndScreen != null)
        {
            if (player1.currentTetromino != null && player2.currentTetromino != null)
            {
                if (player1.currentTetromino.endGame)
                {
                    if (!player1EndScreen.activeSelf) player1EndScreen.SetActive(true);
                }

                if (player2.currentTetromino.endGame)
                {
                    if (!player2EndScreen.activeSelf) player2EndScreen.SetActive(true);
                }
            }
        }
    }

    IEnumerator FindPlayers()
    {
        yield return new WaitUntil(() => FindObjectsByType<Player>(FindObjectsSortMode.None).Length > 0);

        Player[] players = FindObjectsByType<Player>(FindObjectsSortMode.None);
        if (players.Length > 0)
        {
            player1 = players[0];
            if (players.Length > 1)
            {
                player2 = players[1];
            }
        }
    }
}
