using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Base : MonoBehaviour, IPoolable<Base>, IBase, ISelectable
{
    private const int MaxAmoutUnit = 6;
    private const int UnitCost = 3;

    [SerializeField] private List<Unit> _units = new();
    [SerializeField] private SelectionIndicator _selectionIndicator;

    private IScanner _scanner;
    private IResourceContainer _resourceContainer;
    private IResourceCounter _resourceCounter;
    private IUnitSpawner _unitSpawner;
    private IConstructionService _constructionService;
    private IFlagHandler _flagHandler;

    private float _assingnUnitDelay = 0.5f;
    private bool _isBaseConstructionInProgress;

    public event Action<Base> OnReleased;
    public event Action CreateUnit;
    
    public SelectionIndicator selectionIndicator => _selectionIndicator;
    public int AmountUnits => _units.Count;
    public bool IsSelected { get; set; }

    [Inject]
    public void Construct(IConstructionService constructionService,
                          IScanner scanner,
                          IResourceContainer resourceContainer,
                          IResourceCounter resourceCounter,
                          IUnitSpawner unitSpawner,
                          IFlagHandler flagHandler,
                          IScoreView scoreView
                          )
    {
        _constructionService = constructionService;
        _scanner = scanner;
        _resourceContainer = resourceContainer;
        _resourceCounter = resourceCounter;
        _unitSpawner = unitSpawner;
        _flagHandler = flagHandler;
    }

    private void Start()
    {
        _scanner.ResourceFound += _resourceContainer.AddResource;
        StartCoroutine(WorkCoroutine());
    }

    public void AddUnit(IUnit unit) =>
        _units.Add((Unit)unit);

    public void CollectResource(Resource resource)
    {
        resource.Release();
        _resourceContainer.Remove(resource);
        _resourceCounter.AddScore();
    }

    public bool TryGetAvailableUnit(out IUnit unit)
    {
        unit = _units.FirstOrDefault(unit => unit.IsBusy == false);
        return unit != null;
    }

    public Vector3 GetBasePosition() =>
        transform.position;

    private void OnDisable()
    {
        _scanner.ResourceFound -= _resourceContainer.AddResource;
        OnReleased?.Invoke(this);
    }

    public IEnumerator WorkCoroutine()
    {
        StartCoroutine(_unitSpawner.SpawnUnits());

        WaitForSeconds wait = new(_assingnUnitDelay);

        while (enabled)
        {
            if (_scanner.IsScanning == false)
                _scanner.StartScan();

            AssignTasksToUnits();
            TrySpawnNewUnit();

            yield return wait;
        }
    }

    public void AssignTasksToUnits()
    {
        if (_constructionService.CanBuildNewBase(this) && _flagHandler.FlagIsActive && _isBaseConstructionInProgress == false)
        {
            if (TryGetAvailableUnit(out IUnit unit))
            {
                _isBaseConstructionInProgress = true;
                unit.CreateBase();
            }
        }
        else
        {
            AssignResourceToUnit();
        }
    }

    public void TrySpawnNewUnit()
    {
        if (_units.Count < MaxAmoutUnit)
        {
            if (_resourceCounter.ResourceAmount >= UnitCost && _flagHandler.FlagIsActive == false)
            {
                _resourceCounter.RemoveScore(UnitCost);
                _unitSpawner.CreateUnit();
            }
        }
    }

    public void AssignResourceToUnit()
    {
        foreach (Unit unit in _units.Where(unit => unit.IsBusy == false).ToList())
        {
            Resource resource = _resourceContainer.FindResource(unit.CurrentPosition);

            if (resource != null && _resourceContainer.TryReserve(resource))
                unit.AssignResource(resource);
        }
    }

    public void Select()
    {
        IsSelected = true;
        selectionIndicator.gameObject.SetActive(true);
    }

    public void Deselect()
    {
        IsSelected = false;
        selectionIndicator.gameObject.SetActive(false);
    }
}