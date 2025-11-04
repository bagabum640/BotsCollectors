using System;
using UnityEngine;

[RequireComponent(typeof(Unit))]
public class Builder : MonoBehaviour, IBuilder
{
    private IConstructionService _construcionService;
    private IFlagHandler _flagHandler;
    private IUnit _unit;

    [field: SerializeField] public bool IsCreating { get; set; }
    public Vector3 FlagPosition
    {
        get
        {
            if (_flagHandler == null)
                throw new Exception("FlagHandler dependency not injected");
            return _flagHandler.GetFlagPosition;
        }
    }

    public void Init(IUnit unit, IConstructionService constructionService, IFlagHandler flagHandler)
    {
        _unit = unit;
        _construcionService = constructionService;
        _flagHandler = flagHandler;
    }

    public void OnCreate() =>
        IsCreating = true;

    public void Create(Vector3 flag)
    {
        if (IsCreating && flag != null)
        {
            _construcionService.CompleteBaseConstruction(_unit, FlagPosition);
            IsCreating = false;
        }
    }
}