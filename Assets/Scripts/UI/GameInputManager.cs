using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputManager : MonoBehaviour
{
    // Not used yet (idea for replacement current tetromino control system)

    public static event Action<Vector2> OnMoveInput;
    public static event Action OnPlayerHardDrop;

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed || context.canceled)
        {
            Vector2 moveInput = context.ReadValue<Vector2>();
            OnMoveInput?.Invoke(moveInput);
        }
    }

    public void OnHardDrop(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnPlayerHardDrop?.Invoke();
        }
    }
}
