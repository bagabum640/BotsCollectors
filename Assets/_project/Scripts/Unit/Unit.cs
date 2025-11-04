using System;
using UnityEngine;

[RequireComponent(typeof(Mover),
                  typeof(ResourcePicker))]
public class Unit : MonoBehaviour, IPoolable<Unit>, IUnit
{
    private IBase _base;
    private IMover _mover;
    private IResourcePicker _picker;
    private IBuilder _builder;
    private UnitStateMachine _stateMachine;

    public event Action<Unit> OnReleased;
    [field: SerializeField] public Resource AssignedResource { get; private set; }
    [field: SerializeField] public bool IsBusy { get; private set; }
    public Vector3 BasePosition => _base.GetBasePosition();
    public Vector3 CurrentPosition => _mover.Agent.transform.position;
    public float InteractDistance { get; private set; } = 4f;

    public void Init(IBase @base,
                     IConstructionService constructionService,
                     IFlagHandler flagHandler)
    {
        _base = @base;
        _mover = GetComponent<Mover>();
        _picker = GetComponent<ResourcePicker>();
        _builder = GetComponent<Builder>();

        _builder.Init(this, constructionService, flagHandler);

        _stateMachine = new(this, _mover, _picker, _builder);
    }


    private void Update() =>
        _stateMachine?.Update();  

    private void OnDisable() =>
        OnReleased?.Invoke(this);

    public void SetStartPosition(Vector3 position) =>
        _mover.Warp(position);

    public void SetBase(IBase @base) =>
        _base = @base;

    public IBase GetBase() =>
        _base;

    public void AssignResource(Resource resource)
    {
        AssignedResource = resource;
        IsBusy = true;
    }

    public void CreateBase()
    {
        _builder.OnCreate();
    }

    public void DeliverResource()
    {
        _base.CollectResource(AssignedResource);
        ClearResource();
    }

    public void ClearResource()
    {
        _picker.DropResource();
        AssignedResource = null;
        IsBusy = false;
    }
}