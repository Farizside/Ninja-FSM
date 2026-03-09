using UnityEngine;

public abstract class NinjaState : ScriptableObject
{
    public StateKey stateKey;

    public abstract void OnEnter(NinjaController ninja);
    public abstract void OnUpdate(NinjaController ninja);
    public abstract void OnExit(NinjaController ninja);
}