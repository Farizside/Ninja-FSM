using UnityEngine;

[CreateAssetMenu(menuName = "Ninja/States/Run")]
public class RunState : NinjaState
{
    public override void OnEnter(NinjaController ninja)
    {
        ninja.Animator.SetBool("isMoving", true);
    }

    public override void OnUpdate(NinjaController ninja)
    {
        if (!ninja.IsAlive) return;

        ninja.Rb.linearVelocity = new Vector2(ninja.MoveInput * ninja.moveSpeed, ninja.Rb.linearVelocity.y);
        ninja.FlipTowardsMovement();

        ninja.Animator.SetBool("isGrounded", ninja.IsGrounded);

        if (ninja.AttackRequested)               { ninja.ChangeState(StateKey.Attack); return; }
        if (ninja.JumpRequested)                 { ninja.ChangeState(StateKey.Jump);   return; }
        if (Mathf.Abs(ninja.MoveInput) < 0.01f)   ninja.ChangeState(StateKey.Idle);
    }

    public override void OnExit(NinjaController ninja)
    {
        ninja.Rb.linearVelocity = new Vector2(0f, ninja.Rb.linearVelocity.y);
        ninja.Animator.SetBool("isMoving", false);
    }
}