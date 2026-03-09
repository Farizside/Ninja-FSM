using UnityEngine;

[CreateAssetMenu(menuName = "Ninja/States/Jump")]
public class JumpState : NinjaState
{
    public override void OnEnter(NinjaController ninja)
    {
        ninja.Rb.linearVelocity = new Vector2(ninja.Rb.linearVelocity.x, ninja.jumpForce);
        ninja.Animator.SetBool("isGrounded", false);
    }

    public override void OnUpdate(NinjaController ninja)
    {
        if (!ninja.IsAlive) return;

        ninja.Rb.linearVelocity = new Vector2(ninja.MoveInput * ninja.moveSpeed, ninja.Rb.linearVelocity.y);
        ninja.FlipTowardsMovement();

        if (ninja.IsGrounded && ninja.Rb.linearVelocity.y <= 0f)
        {
            ninja.Animator.SetBool("isGrounded", true);
            ninja.ChangeState(Mathf.Abs(ninja.MoveInput) > 0.01f ? StateKey.Run : StateKey.Idle);
        }
    }

    public override void OnExit(NinjaController ninja) { }
}