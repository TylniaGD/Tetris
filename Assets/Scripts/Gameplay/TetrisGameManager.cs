using System;
using UnityEngine;

public class TetrisGameManager : MonoBehaviour
{
    public static event Action<Player, int> OnNextTetrominoChanged;

    [SerializeField] GameObject[] tetrominoes;

    [Header("Players")]
    [SerializeField] Player player1;
    [SerializeField] Player player2;

    [Space]

    public int currentTetrominoID;
    public int nextTetrominoID;

    [Space]

    [HideInInspector] public bool isNameInputCompleted;
    [HideInInspector] public int numberDrawn;
    bool startTetrominoCreated = false;

    void Update()
    {
        CreateInitialTetromino();
    }

    public void SpawnNewTetromino(Player player)
    {
        Vector3 spawnPosition = new(player.transform.position.x, player.transform.position.y, player.transform.position.z);

        GameObject newTetromino = Instantiate(tetrominoes[player.nextTetrominoID], spawnPosition, Quaternion.identity);

        int randomIndex = UnityEngine.Random.Range(0, tetrominoes.Length);
        player.nextTetrominoID = randomIndex;

        OnNextTetrominoChanged?.Invoke(player, randomIndex);

        newTetromino.layer = player.gameObject.layer;
        player.SetCurrentTetromino(newTetromino);
    }

    void CreateInitialTetromino()
    {
        if (isNameInputCompleted && !startTetrominoCreated)
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
}

