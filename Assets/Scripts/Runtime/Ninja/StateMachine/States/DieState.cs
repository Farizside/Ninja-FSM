using UnityEngine;

[CreateAssetMenu(menuName = "Ninja/States/Die")]
public class DieState : NinjaState
{
    public override void OnEnter(NinjaController ninja)
    {
        ninja.Animator.SetTrigger("die");
        ninja.Rb.linearVelocity = Vector2.zero;
    }

    public override void OnUpdate(NinjaController ninja) { }
    public override void OnExit(NinjaController ninja) { }
}