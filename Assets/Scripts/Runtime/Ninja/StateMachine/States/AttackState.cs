using UnityEngine;

[CreateAssetMenu(menuName = "Ninja/States/Attack")]
public class AttackState : NinjaState
{
    public float attackDuration = 0.4f;
    
    public override void OnEnter(NinjaController ninja)
    {
        ninja.Animator.SetTrigger("attack");
        ninja.StateTimer = attackDuration;
    }

    public override void OnUpdate(NinjaController ninja)
    {
        ninja.StateTimer -= Time.deltaTime;

        if (ninja.StateTimer <= 0f)
        {
            bool moving = Mathf.Abs(ninja.MoveInput) > 0.01f;
            ninja.ChangeState(ninja.IsGrounded
                ? (moving ? StateKey.Run : StateKey.Idle)
                : StateKey.Jump);
        }
    }

    public override void OnExit(NinjaController ninja) { }
}