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

    public List<Transform> player1ActiveBlocks = new();
    public List<Transform> player2ActiveBlocks = new();

    [Space]

    public float gridSize = 0.5f;
    public float columns = 22;
    public float rows = 15;

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
        Vector3 spawnPosition = new(player.transform.position.x, player.transform.position.y - 1.8f, player.transform.position.z);

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
                AddTetrominoElementsToActiveBlocks(stoppedTetromino.transform, player1ActiveBlocks);
                CheckAndClearLines(player1ActiveBlocks);
                SpawnNewTetromino(player1);
                return;
            }
        }
        else
        {
            Debug.Log("koniec gry dla player1");
            player1.isEndGame = true;
        }

        if (IsSpawnPointClear(player2))
        {
            if (tetrominoLayer == LayerMask.NameToLayer("Player2"))
            {
                AddTetrominoElementsToActiveBlocks(stoppedTetromino.transform, player2ActiveBlocks);
                CheckAndClearLines(player2ActiveBlocks);
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
        Collider2D hit = Physics2D.OverlapBox(player.transform.position, new Vector2(1f, 1f), 0);
        return hit == null;
    }

    void AddTetrominoElementsToActiveBlocks(Transform tetromino, List<Transform> activeBlocks)
    {
        foreach (Transform block in tetromino)
        {
            activeBlocks.Add(block);
        }
    }

    public void ClearTetrominoElements(List<Transform> activeBlocks)
    {
        foreach (Transform block in activeBlocks)
        {
            Destroy(block.gameObject);
        }
    }

    void CheckAndClearLines(List<Transform> activeBlocks)
    {
        if (activeBlocks.Count == 0) return;

        bool linesCleared;

        do
        {
            linesCleared = false;

            float minY = activeBlocks.Min(block => block.position.y);
            float maxY = activeBlocks.Max(block => block.position.y);

            List<float> linesToClear = new();

            for (float y = minY; y <= maxY; y += gridSize)
            {
                if (IsLineFull(y, activeBlocks))
                {
                    linesToClear.Add(y);
                }
            }

            if (linesToClear.Count > 0)
            {
                foreach (float y in linesToClear)
                {
                    ClearLine(y, activeBlocks);
                }

                ShiftBlocksDown(linesToClear.Min(), activeBlocks);

                linesCleared = true;
            }

        } while (linesCleared);
    }

    bool IsLineFull(float y, List<Transform> activeBlocks)
    {
        int blockCount = activeBlocks.Count(block => Mathf.Abs(block.position.y - y) < 0.15f);
        return blockCount >= 10; // Player grid width
    }

    void ClearLine(float y, List<Transform> activeBlocks)
    {
        activeBlocks.RemoveAll(block =>
        {
            if (Mathf.Abs(block.position.y - y) < 0.1f)
            {
                Renderer blockRenderer = block.GetComponent<Renderer>();
                if (blockRenderer != null)
                {
                    Color blockColor = blockRenderer.material.color;
                    blockColor.a = 0.2f;
                    blockRenderer.material.color = blockColor;
                }
                Destroy(block.gameObject, 0.15f);
                return true;
            }
            return false;
        });
    }

    void ShiftBlocksDown(float fromY, List<Transform> activeBlocks)
    {
        List<Transform> updatedBlocks = new(activeBlocks.Count);

        foreach (Transform block in activeBlocks)
        {
            if (block.position.y > fromY)
            {
                block.position -= new Vector3(0, gridSize, 0);
            }

            block.position = new Vector3(
                block.position.x,
                Mathf.Round(block.position.y / gridSize) * gridSize,
                block.position.z
            );

            updatedBlocks.Add(block);
        }

        activeBlocks.Clear();
        activeBlocks.AddRange(updatedBlocks);
    }



    public Vector2 gridOffset = new(0, 0);
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        for (int x = 0; x <= columns; x++)
        {
            float posX = x * gridSize + gridOffset.x;
            Gizmos.DrawLine(new Vector3(posX, gridOffset.y, 0), new Vector3(posX, rows * gridSize + gridOffset.y, 0));
        }
        for (int y = 0; y <= rows; y++)
        {
            float posY = y * gridSize + gridOffset.y;
            Gizmos.DrawLine(new Vector3(gridOffset.x, posY, 0), new Vector3(columns * gridSize + gridOffset.x, posY, 0));
        }
    }

}