public class IdleState : UnitState
{
    private readonly Builder _builder;

    public IdleState(Unit unit,Builder builder, IStateChanger stateChanger) : base(unit, stateChanger) 
    { 
        _builder = builder;
    }

    public override void Update()
    {
        if (Unit.IsBusy)
            StateChanger.SetState<GatheringState>();

        if (_builder.IsCreating)        
            StateChanger.SetState<BuildState>();       
    }
}