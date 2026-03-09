using System.Collections.Generic;
using UnityEngine;

public class NinjaStateMachine
{
    public NinjaState CurrentState { get; private set; }

    private readonly Dictionary<StateKey, NinjaState> _states = new();
    private readonly NinjaController _ninja;

    public NinjaStateMachine(NinjaController ninja)
    {
        _ninja = ninja;
    }
    
    public void Register(NinjaState state)
    {
        if (state == null)
        {
            Debug.LogWarning("[FSM] Tried to register a null state.");
            return;
        }

        if (_states.ContainsKey(state.stateKey))
            Debug.LogWarning($"[FSM] Duplicate key '{state.stateKey}' — overwriting.");

        _states[state.stateKey] = state;
    }

    public void RegisterAll(IEnumerable<NinjaState> states)
    {
        foreach (var s in states)
            Register(s);
    }
    
    public void ChangeState(StateKey key)
    {
        if (!_states.TryGetValue(key, out var next))
        {
            return;
        }

        if (next == CurrentState) return;

        CurrentState?.OnExit(_ninja);
        CurrentState = next;
        CurrentState.OnEnter(_ninja);
    }
    
    public void Update() => CurrentState?.OnUpdate(_ninja);
    
    public bool HasState(StateKey key) => _states.ContainsKey(key);
}