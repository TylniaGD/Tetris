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

    bool startTetrominoCreated = false;

    void Awake()
    {
        StartCoroutine(FindSetPlayerNames());
    }

    void Update()
    {
        CreateInitialTetromino();

        // TODO: Add logic to create a new block after the previous one falls 
    }

    public void SpawnNewTetromino(Player player)
    {
        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPosition = new(player.transform.position.x + randomX, player.transform.position.y, player.transform.position.z);

        int randomIndex = Random.Range(0, tetrominoes.Length);
        GameObject newTetromino = Instantiate(tetrominoes[randomIndex], spawnPosition, Quaternion.identity);

        newTetromino.layer = player.gameObject.layer;

        player.SetCurrentTetromino(newTetromino);
    }

    void CreateInitialTetromino()
    {
        if (setPlayerNames != null && setPlayerNames.isNameInputCompleted && !startTetrominoCreated)
        {
            startTetrominoCreated = true;

            SpawnNewTetromino(player1);
            SpawnNewTetromino(player2);
        }
    }

    IEnumerator FindSetPlayerNames()
    {
        yield return new WaitUntil(() => FindAnyObjectByType<SetPlayerNames>() != null);
        setPlayerNames = FindAnyObjectByType<SetPlayerNames>();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(player1.transform.position, maxX);
    }
}
