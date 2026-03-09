using UnityEngine;

[CreateAssetMenu(menuName = "Ninja/States/Hurt")]
public class HurtState : NinjaState
{
    public float hurtDuration = 0.3f;

    public override void OnEnter(NinjaController ninja)
    {
        ninja.Animator.SetTrigger("hurt");
        ninja.Rb.linearVelocity = Vector2.zero;
        ninja.StateTimer = hurtDuration;
    }

    public override void OnUpdate(NinjaController ninja)
    {
        ninja.StateTimer -= Time.deltaTime;
        if (ninja.StateTimer <= 0f) ninja.ChangeState(StateKey.Idle);
    }

    public override void OnExit(NinjaController ninja) { }
}