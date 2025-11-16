using UnityEngine;

public class UnitState
{
    protected Unit Unit;
    protected IStateChanger StateChanger;

    public UnitState(Unit unit, IStateChanger stateChanger)
    {
        Unit = unit;
        StateChanger = stateChanger;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }

    protected virtual bool IsInRange(Vector3 target, Vector3 currentPosition, float range)=>
        (target - currentPosition).sqrMagnitude <= range;
}