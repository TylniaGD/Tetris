using System;
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
            if (player1 != null && player2 != null)
            {
                if (player1.isEndGame)
                {
                    if (!player1EndScreen.activeSelf) player1EndScreen.SetActive(true);
                }

                if (player2.isEndGame)
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

        foreach (Player player in players)
        {
            if (player.playerID == 1 && player1 == null)
            {
                player1 = player;
            }
            else if (player.playerID == 2 && player2 == null)
            {
                player2 = player;
            }
        }
    }
}
