using System;
using UnityEngine;

public class TetrisGameManager : MonoBehaviour
{
    public static event Action<Player, int> OnNextTetrominoChanged;
    Transform[,] grid = new Transform[10, 13];
    Tetromino tetrominoManager;

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
    bool isAddedToGrid = false;

    void Update()
    {
        CreateInitialTetromino();

        if (tetrominoManager != null)
        {
            if (tetrominoManager.isLanded && !isAddedToGrid)
            {
                foreach (Transform block in tetrominoManager.transform)
                {
                    Vector2 pos = new(Mathf.Round(block.position.x), Mathf.Round(block.position.y));
                    Debug.Log($"Block Position: {pos.x}, {pos.y}");
                    grid[(int)pos.x, (int)pos.y] = block;
                }

                isAddedToGrid = true;

                CheckAndClearFullLines();
            }
        }
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

    public void SpawnNewTetromino(Player player)
    {
        Vector3 spawnPosition = new(player.transform.position.x, player.transform.position.y - 1.1f, player.transform.position.z);

        GameObject newTetromino = Instantiate(tetrominoes[player.nextTetrominoID], spawnPosition, Quaternion.identity);

        tetrominoManager = newTetromino.GetComponent<Tetromino>();

        int randomIndex = UnityEngine.Random.Range(0, tetrominoes.Length);
        player.nextTetrominoID = randomIndex;

        OnNextTetrominoChanged?.Invoke(player, randomIndex);

        newTetromino.layer = player.gameObject.layer;
        player.SetCurrentTetromino(newTetromino);

        isAddedToGrid = false;
    }

    public void AssignNextTetromino(GameObject stoppedTetromino)
    {
        int tetrominoLayer = stoppedTetromino.layer;

        if (IsSpawnPointClear(player1))
        {
            if (tetrominoLayer == LayerMask.NameToLayer("Player1"))
            {
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

    // --

    void CheckAndClearFullLines()
    {
        for (int y = 0; y < grid.GetLength(1); y++)
        {
            if (IsLineFull(y))
            {
                ClearLine(y);
            }
        }
    }
    bool IsLineFull(int y)
    {
        for (int x = 0; x < 10; x++)
        {
            if (grid[x, y] == null) return false;
        }
        return true;
    }
    void ClearLine(int y)
    {
        for (int x = 0; x < 10; x++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }
}

