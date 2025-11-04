public class IdleState : UnitState
{
    private readonly IBuilder _builder;

    public IdleState(IUnit unit, IBuilder builder, IStateChanger stateChanger) : base(unit, stateChanger) 
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