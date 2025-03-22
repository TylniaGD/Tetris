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

        if (players.Length > 0)
        {
            // Sort so that the player further to the left (smaller X) is player1
            Array.Sort(players, (p1, p2) => p1.transform.position.x.CompareTo(p2.transform.position.x));

            player1 = players[0];
            if (players.Length > 1)
            {
                player2 = players[1];
            }
        }
    }
}
