using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitStateMachine : IStateChanger
{
    private readonly Dictionary<Type, Func<UnitState>> _states = new();
    private UnitState _currentState;

    public UnitStateMachine(IUnit unit, IMover mover, IResourcePicker picker, IBuilder builder)
    {
        _states = new Dictionary<Type, Func<UnitState>>
        {
            [typeof(IdleState)] = () => new IdleState(unit, builder, this),
            [typeof(GatheringState)] = () => new GatheringState(unit, mover, picker, this),
            [typeof(BuildState)] = () => new BuildState(unit, mover, builder, this)
        };
        
        SetState<IdleState>();
    }

    public void Update() =>
        _currentState.Update();

    public void SetState<TState>() where TState : UnitState
    {
        Debug.Log($"<color=#4DA6FF><b>[StateMachine]</b></color> " +
                  $"<color=#FFCC00>Transitioning from</color> " +
                  $"<color=#FF6666>{_currentState?.GetType().Name ?? "NULL"}</color> " +
                  $"<color=#FFCC00>to</color> " +
                  $"<color=#66FF66>{typeof(TState).Name}</color>");

        if (_states.TryGetValue(typeof(TState), out var nextState))
        {
            _currentState?.Exit();
            _currentState = nextState();
            _currentState?.Enter();
        }
    }
}