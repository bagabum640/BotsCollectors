using UnityEngine;

public class BuildState : UnitState
{
    private readonly IMover _mover;
    private readonly IBuilder _builder;

    private bool _isBuildingCompleted;

    public BuildState(IUnit unit, IMover mover, IBuilder builder, IStateChanger stateChanger) : base(unit, stateChanger)
    {
        _mover = mover;
        _builder = builder;
    }

    public override void Enter()
    {
        _isBuildingCompleted = false;
        _mover.MoveTo(_builder.FlagPosition);
    }

    public override void Update()
    {
        if (_builder.IsCreating == false || _isBuildingCompleted)
            StateChanger.SetState<IdleState>();

        if (IsInRange(_builder.FlagPosition, Unit.CurrentPosition, Unit.InteractDistance))
        {
            StartBuilding();
        }
    }

    private void StartBuilding()
    {
        if (_isBuildingCompleted == false)
        {
            Debug.Log("Начал постройку");
            _builder.Create(_builder.FlagPosition);
            _isBuildingCompleted = true;
        }
    }
}