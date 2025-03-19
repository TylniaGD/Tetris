using System.Collections;
using UnityEngine;

public class TetrisGameManager : MonoBehaviour
{
    [SerializeField] SetPlayerNames setPlayerNames;
    [SerializeField] GameObject[] tetrominoes;

    [Space]

    [SerializeField] Player player1;
    [SerializeField] Player player2;

    [Space]

    [SerializeField] float newTetrominoCooldown = 6.5f;

    [Space]

    [SerializeField] float maxX = 2.74f;
    [SerializeField] float minX = -2.74f;

    void Awake()
    {
        StartCoroutine(FindSetPlayerNames());

        StartCoroutine(SpawnNewTetromino(player1));
        StartCoroutine(SpawnNewTetromino(player2));
    }

    IEnumerator FindSetPlayerNames()
    {
        yield return new WaitUntil(() => FindAnyObjectByType<SetPlayerNames>() != null);
        setPlayerNames = FindAnyObjectByType<SetPlayerNames>();
    }

    IEnumerator SpawnNewTetromino(Player player)
    {
        while (true)
        {
            if (setPlayerNames != null && setPlayerNames.isNameInputCompleted)
            {
                float randomX = Random.Range(minX, maxX);
                Vector3 spawnPosition = new(player.transform.position.x + randomX, player.transform.position.y, player.transform.position.z);

                int randomIndex = Random.Range(0, tetrominoes.Length);
                GameObject newTetromino = Instantiate(tetrominoes[randomIndex], spawnPosition, Quaternion.identity);
                player.SetCurrentTetromino(newTetromino);

                yield return new WaitForSeconds(newTetrominoCooldown);
            }
            else
            {
                yield return null;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(player1.transform.position, maxX);
    }
}
