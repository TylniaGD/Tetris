using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    TetrisGameManager tetrisGameManager;

    public int playerID;

    [Space]

    public GameObject currentTetrominoObject;
    public Tetromino currentTetromino;

    [Space]

    public int nextTetrominoID;

    [Space]

    [SerializeField] float moveSpeed = 1.2f;

    [Space]

    public bool isEndGame = false;

    Vector2 moveInput;
    bool areStartedComponentsFound = false;
    readonly float rotationAmount = 90f;

    void Start()
    {
        tetrisGameManager = FindAnyObjectByType<TetrisGameManager>();
    }

    void Update()
    {
        if (currentTetrominoObject != null)
        {
            if (!areStartedComponentsFound)
            {
                areStartedComponentsFound = true;
                SetComponents();
            }

            if (currentTetromino.isLanded && !isEndGame)
            {
                SnapToGrid();
                tetrisGameManager.AssignNextTetromino(currentTetrominoObject);
                return;
            }
        }
    }

    void FixedUpdate()
    {
        if (currentTetrominoObject != null && rb.bodyType != RigidbodyType2D.Static && !isEndGame)
        {
            rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
        }
    }

    void SnapToGrid()
    {
        Vector3 firstBlockPos = transform.GetChild(0).position;
        Vector3 offset = transform.position - firstBlockPos;

        Vector3 snappedPosition = new(
            Mathf.Round((transform.position.x - offset.x) / tetrisGameManager.gridSize) * tetrisGameManager.gridSize + offset.x,
            Mathf.Round((transform.position.y - offset.y) / tetrisGameManager.gridSize) * tetrisGameManager.gridSize + offset.y,
            0
        );

        transform.position = snappedPosition;
    }

    public void SetCurrentTetromino(GameObject tetromino)
    {
        currentTetrominoObject = tetromino;
        SetComponents();
    }

    void SetComponents()
    {
        rb = currentTetrominoObject.GetComponent<Rigidbody2D>();
        currentTetromino = currentTetrominoObject.GetComponent<Tetromino>();
    }

    // ----------
    // This should be in the GameInputManager script in UIScene assembly

    public void HardDrop(InputAction.CallbackContext context)
    {
        if (context.performed && tetrisGameManager.isNameInputCompleted)
        {
            rb.gravityScale = 15f;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    // In the future, implement Super Rotation System (SRS) for correct rotation
    public void Rotate(InputAction.CallbackContext context)
    {
        if (context.performed && !isEndGame && tetrisGameManager.isNameInputCompleted)
        {
            string key = context.control.name;
            if (key == "e" || key == "period") currentTetromino.transform.Rotate(0, 0, -rotationAmount);
            if (key == "q" || key == "comma") currentTetromino.transform.Rotate(0, 0, rotationAmount);
        }
    }

    void OnDrawGizmos()
    {
        foreach (var block in FindObjectsByType<Tetromino>(FindObjectsSortMode.None))
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(block.transform.position, 0.1f);
        }
    }
}