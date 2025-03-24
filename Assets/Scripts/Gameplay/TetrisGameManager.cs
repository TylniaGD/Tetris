using System;
using System.Collections.Generic;
using System.Linq;
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

    [Space]

    public List<Transform> activeBlocks = new();
    readonly float rowHeight = 0.7f;
    readonly int playfieldWidth = 11;

    void Update()
    {
        if (isNameInputCompleted && !startTetrominoCreated)
        {
            startTetrominoCreated = true;

            CreateInitialTetromino(player1);
            CreateInitialTetromino(player2);
        }
    }

    void CreateInitialTetromino(Player player)
    {
        player.nextTetrominoID = UnityEngine.Random.Range(0, tetrominoes.Length);

        SpawnNewTetromino(player);
    }

    void SpawnNewTetromino(Player player)
    {
        Vector3 spawnPosition = new(player.transform.position.x, player.transform.position.y - 1.6f, player.transform.position.z);

        GameObject newTetromino = Instantiate(tetrominoes[player.nextTetrominoID], spawnPosition, Quaternion.identity, player.transform);

        int randomIndex = UnityEngine.Random.Range(0, tetrominoes.Length);
        player.nextTetrominoID = randomIndex;

        OnNextTetrominoChanged?.Invoke(player, randomIndex); // Change UI for next tetromino

        newTetromino.layer = player.gameObject.layer;
        player.SetCurrentTetromino(newTetromino);
    }

    public void AssignNextTetromino(GameObject stoppedTetromino)
    {
        int tetrominoLayer = stoppedTetromino.layer;

        if (IsSpawnPointClear(player1))
        {
            if (tetrominoLayer == LayerMask.NameToLayer("Player1"))
            {
                AddTetrominoElementsToActiveBlocks(stoppedTetromino.transform);
                CheckAndClearLines();
                SpawnNewTetromino(player1);
                return;
            }
        }
        else
        {
            player1.isEndGame = true;
        }

        if (IsSpawnPointClear(player2))
        {
            if (tetrominoLayer == LayerMask.NameToLayer("Player2"))
            {
                AddTetrominoElementsToActiveBlocks(stoppedTetromino.transform);
                CheckAndClearLines();
                SpawnNewTetromino(player2);
                return;
            }
        }
        else
        {
            player2.isEndGame = true;
        }
    }

    bool IsSpawnPointClear(Player player)
    {
        Collider2D hit = Physics2D.OverlapBox(player.transform.position, new Vector2(0.5f, 0.5f), 0);
        return hit == null;
    }

    void AddTetrominoElementsToActiveBlocks(Transform tetromino)
    {
        foreach (Transform block in tetromino)
        {
            activeBlocks.Add(block);
        }
    }

     // ---

    void CheckAndClearLines()
    {
        if (activeBlocks.Count == 0) return;

        float minY = activeBlocks.Min(block => block.position.y);
        float maxY = activeBlocks.Max(block => block.position.y);

        float y = minY;

        while (y <= maxY)
        {
            if (IsLineFull(y))
            {
                ClearLine(y);
                ShiftBlocksDown(y);
                continue; // Recheck the same line after shifting
            }
            y += rowHeight;
        }
    }

    bool IsLineFull(float y)
    {
        int blockCount = activeBlocks.Count(block => Mathf.Abs(block.position.y - y) < 0.1f);
        return blockCount >= playfieldWidth;
    }

    void ClearLine(float y)
    {
        activeBlocks.RemoveAll(block =>
        {
            if (Mathf.Abs(block.position.y - y) < 0.1f)
            {
                Destroy(block.gameObject);
                return true;
            }
            return false;
        });
    }

    void ShiftBlocksDown(float fromY)
    {
        foreach (Transform block in activeBlocks)
        {
            if (block.position.y > fromY)
            {
                block.position -= new Vector3(0, rowHeight, 0);
            }
        }
    }
}