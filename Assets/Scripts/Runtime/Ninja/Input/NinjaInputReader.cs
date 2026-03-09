using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class NinjaInputReader : MonoBehaviour
{
    #region Events

    public event Action<float> OnMoveInput;
    public event Action OnJumpPressed;
    public event Action OnAttackPressed;
    public event Action OnTakeDamagePressed;

    #endregion

    #region Input Callbacks

    public void OnMove(InputAction.CallbackContext ctx)
    {
        OnMoveInput?.Invoke(ctx.ReadValue<Vector2>().x);
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) OnJumpPressed?.Invoke();
    }

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) OnAttackPressed?.Invoke();
    }

    public void OnTakeDamage(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) OnTakeDamagePressed?.Invoke();
    }

    #endregion
}