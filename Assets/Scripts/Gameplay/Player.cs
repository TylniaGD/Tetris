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

    // -----XXXxxxXXX-----
    // This should be in the code GameInputManager in UIScene assembly

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

 
}