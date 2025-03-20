using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    TetrisGameManager tetrisGameManager;

    public GameObject currentTetrominoObject;
    public Tetromino currentTetromino;

    public int nextTetrominoID;

    [Space]

    [SerializeField] float moveSpeed = 2f;

    Vector2 moveInput;
    bool areStartedComponentsFound = false;

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

            if (currentTetromino.isLanded)
            {
                tetrisGameManager.AssignNextTetromino(currentTetrominoObject);
                return;
            }
        }
    }

    void FixedUpdate()
    {
        if (currentTetrominoObject != null && rb.bodyType != RigidbodyType2D.Static)
            rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
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

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    // TODO: Add method for accelerating blocks, etc.
}
