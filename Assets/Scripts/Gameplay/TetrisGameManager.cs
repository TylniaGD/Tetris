using System.Collections;
using UnityEngine;

public class TetrisGameManager : MonoBehaviour
{
    SetPlayerNames setPlayerNames;

    [SerializeField] GameObject[] tetrominoes;


    [Header("Players")]
    [SerializeField] Player player1;
    [SerializeField] Player player2;


    [Header("Respawn Scope")]
    [SerializeField] float maxX = 2.74f;
    [SerializeField] float minX = -2.74f;

    bool startTetrominoCreated = false;
    LayerMask newTetrominoLayer;

    void Awake()
    {
        StartCoroutine(FindSetPlayerNames());
    }

    void Update()
    {
        CreateInitialTetromino();
    }

    public void SpawnNewTetromino(Player player)
    {
        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPosition = new(player.transform.position.x + randomX, player.transform.position.y, player.transform.position.z);

        int randomIndex = Random.Range(0, tetrominoes.Length);
        GameObject newTetromino = Instantiate(tetrominoes[randomIndex], spawnPosition, Quaternion.identity);

        newTetromino.layer = player.gameObject.layer;
        newTetrominoLayer = newTetromino.layer;

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

    public void AssignNextTetromino(GameObject stoppedTetromino)
    {
        int tetrominoLayer = stoppedTetromino.layer;

        if (tetrominoLayer == LayerMask.NameToLayer("Player1"))
        {
            SpawnNewTetromino(player1);
            return;
        }
        else if (tetrominoLayer == LayerMask.NameToLayer("Player2"))
        {
            SpawnNewTetromino(player2);
            return;
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
