using System.Diagnostics;

public class IdleState : UnitState
{
    public IdleState(Unit unit, IStateChanger stateChanger) : base(unit, stateChanger) { }

    public override void Update()
    {
        if (Unit.IsBusy)
            StateChanger.SetState<GatheringState>();
    }
}