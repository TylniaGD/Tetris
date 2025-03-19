using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;

    public GameObject currentTetromino;

    [Space]

    [SerializeField] float moveSpeed = 2f;

    Vector2 moveInput;
    bool isRbFound = false;

    void Update()
    {
        if (currentTetromino != null && !isRbFound)
        {
            rb = currentTetromino.GetComponent<Rigidbody2D>();
            isRbFound = true;
        }
    }

    void FixedUpdate()
    {
        if (currentTetromino != null) rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
    }

    public void SetCurrentTetromino(GameObject tetromino)
    {
        currentTetromino = tetromino;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }


}
