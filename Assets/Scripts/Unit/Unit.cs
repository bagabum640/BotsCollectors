using System;
using UnityEngine;

[RequireComponent(typeof(Mover),
                  typeof(ResourcePicker))]
public class Unit : MonoBehaviour, IPoolable<Unit>
{
    private Base _base;
    private Mover _mover;
    private ResourcePicker _picker;
    private UnitStateMachine _stateMachine;

    public event Action<Unit> OnReleased;

    [field: SerializeField] public Resource AssignedResource { get; private set; }
    [field: SerializeField] public bool IsBusy { get; private set; }

    public float InteractDistance { get; private set; } = 4f;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _picker = GetComponent<ResourcePicker>();

        _stateMachine = new(this, _mover, _picker);
    }

    private void OnEnable() =>
        _stateMachine.SetState<IdleState>();

    private void Update() =>   
        _stateMachine.Update();    

    private void OnDisable() =>
        OnReleased?.Invoke(this);

    public void SetStartPosition(Vector3 position) =>
        _mover.Warp(position);

    public Vector3 GetPositionBase() =>
         _base.transform.position;

    public void SetBase(Base @base) =>
        _base = @base;

    public void AssignResource(Resource resource)
    {
        AssignedResource = resource;
        IsBusy = true;
    }

    public void DeliverResource()
    {
        _base.CollectResource(AssignedResource);
        ClearResource();
    }

    private void ClearResource()
    {
        _picker.DropResource();
        AssignedResource = null;
        IsBusy = false;
    }
}