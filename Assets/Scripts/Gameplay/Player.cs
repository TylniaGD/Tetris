using System;
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
                tetrisGameManager.AssignNextTetromino(currentTetrominoObject);
                return;
            }
        }
    }

    void FixedUpdate()
    {
        if (currentTetrominoObject != null && rb.bodyType != RigidbodyType2D.Static && !isEndGame)
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

    // ----------
    // This should be in the GameInputManager script in UIScene assembly

    public void HardDrop(InputAction.CallbackContext context)
    {
        if (context.performed)
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
        if (context.performed && !isEndGame)
        {
            string key = context.control.name;
            if (key == "e" || key == "period") currentTetromino.transform.Rotate(0, 0, -rotationAmount);
            if (key == "q" || key == "comma") currentTetromino.transform.Rotate(0, 0, rotationAmount);
        }
    }
}