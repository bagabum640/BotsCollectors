public class BuildState : UnitState
{
    private readonly Mover _mover;
    private readonly Builder _builder;

    private bool _isBuildingCompleted;

    public BuildState(Unit unit, Mover mover, Builder builder, IStateChanger stateChanger) : base(unit, stateChanger)
    {
        _mover = mover;
        _builder = builder;
    }

    public override void Enter()
    {
        _isBuildingCompleted = false;
        _mover.MoveTo(Unit.FlagPosition);
    }

    public override void Update()
    {
        if (_builder.IsCreating == false || _isBuildingCompleted)
            StateChanger.SetState<IdleState>();

        if (IsInRange(Unit.FlagPosition, Unit.transform.position, Unit.InteractDistance))
        {
            StartBuilding();
        }
    }

    private void StartBuilding()
    {
        if (_isBuildingCompleted == false)
        {
            _builder.Create(Unit.FlagPosition);
            _isBuildingCompleted = true;
        }
    }
}