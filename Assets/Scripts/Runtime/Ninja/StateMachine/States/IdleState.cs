using UnityEngine;

[CreateAssetMenu(menuName = "Ninja/States/Idle")]
public class IdleState : NinjaState
{
    public override void OnEnter(NinjaController ninja)
    {
        ninja.Rb.linearVelocity = new Vector2(0f, ninja.Rb.linearVelocity.y);
        ninja.Animator.SetBool("isMoving", false);
    }

    public override void OnUpdate(NinjaController ninja)
    {
        if (!ninja.IsAlive) return;

        ninja.Animator.SetBool("isGrounded", ninja.IsGrounded);

        if (ninja.AttackRequested) { ninja.ChangeState(StateKey.Attack); return; }
        if (ninja.JumpRequested) { ninja.ChangeState(StateKey.Jump); return; }
        if (Mathf.Abs(ninja.MoveInput) > 0.01f) ninja.ChangeState(StateKey.Run);
    }

    public override void OnExit(NinjaController ninja) { }
}