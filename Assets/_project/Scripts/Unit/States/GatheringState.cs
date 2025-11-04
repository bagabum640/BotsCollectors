using UnityEngine;

public class GatheringState : UnitState
{
    private const float BaseDeliveryDistance = 8f;

    private readonly IMover _mover;
    private readonly IResourcePicker _picker;

    public GatheringState(IUnit unit, IMover mover, IResourcePicker picker, IStateChanger stateChanger) : base(unit, stateChanger)
    {
        _mover = mover;
        _picker = picker;
    }

    public override void Enter()
    {
         MoveToResource();
    } 

    public override void Update()
    {
        if (Unit.AssignedResource == null)
        {
            StateChanger.SetState<IdleState>();
            return;
        }

        if (_picker.HasResource == false)
        {
            if (IsInRange(Unit.AssignedResource.transform.position, Unit.CurrentPosition, Unit.InteractDistance))
            {
                _picker.PickResource(Unit.AssignedResource);
                MoveToBase();
            }
        }
        else if (IsInRange(Unit.BasePosition,Unit.CurrentPosition, BaseDeliveryDistance))
        {
            Unit.DeliverResource();
        }
    }

    public override void Exit() =>
        _mover.Stop();

    private void MoveToResource() =>
        _mover.MoveTo(Unit.AssignedResource.transform.position); 

    private void MoveToBase() =>
        _mover.MoveTo(Unit.BasePosition);
}