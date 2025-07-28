using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Base : MonoBehaviour, IPoolable<Base>
{
    private const int MaxAmoutUnit = 6;
    private const int UnitCost = 3;
    private const int BaseCost = 5;
    private const int MinAmountUnitsForCreateNewBase = 1;

    [SerializeField] private List<Unit> _units;
    [SerializeField] private Scanner _scanner;
    [SerializeField] private ResourceContainer _resourceContainer;
    [SerializeField] private ResourceCounter _resourceCounter;
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private Flag _flag;
    [SerializeField] private SelectionIndicator _selection;
    [SerializeField] private float _assingnUnitDelay = 0.5f;

    private bool _isBaseConstructionInProgress;

    public event Action<Base> OnReleased;

    public SelectionIndicator Selection => _selection;
    public bool FlagIsActive => _flag.gameObject.activeInHierarchy;

    public void Init(BaseSpawner baseSpawner)
    {
        _unitSpawner.Init(baseSpawner);
    }

    private void Start()
    {
        _resourceContainer.Init(this);
        _scanner.ResourceFound += _resourceContainer.AddResource;

        StartCoroutine(WorkCoroutine());
    }

    public void AddUnit(Unit unit) =>
        _units.Add(unit);

    public Vector3 GetFlagPosition() =>
        _flag.transform.position;

    public bool CanBuildNewBase() =>
        _resourceCounter.ResourceAmount >= BaseCost && _units.Count > MinAmountUnitsForCreateNewBase;

    public void SetFlagPosition(Vector3 newPosition) =>
        _flag.transform.position = newPosition;

    public void ActiveFlag()
    {
        if (_units.Count > MinAmountUnitsForCreateNewBase)
            _flag.gameObject.SetActive(true);
    }

    public void DeactiveFlag()
    {
        _isBaseConstructionInProgress = false;
        _flag.gameObject.SetActive(false);
    }

    public void CollectResource(Resource resource)
    {
        resource.Release();
        _resourceContainer.Remove(resource);
        _resourceCounter.AddScore();       
    }

    private IEnumerator WorkCoroutine()
    {
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

    private void AssignTasksToUnits()
    {
        if (FlagIsActive && CanBuildNewBase() && _isBaseConstructionInProgress == false)
            CreateNewBase();
        else
            AssignResourceToUnit();
    }

    private void TrySpawnNewUnit()
    {
        if (_units.Count < MaxAmoutUnit && FlagIsActive == false)
        {
            if (_resourceCounter.ResourceAmount >= UnitCost)
            {
                _resourceCounter.RemoveScore(UnitCost);
                _unitSpawner.Spawn();
            }
        }
    }

    private void CreateNewBase()
    {
        foreach (Unit unit in _units.Where(unit => unit.IsBusy == false))
        {
            _isBaseConstructionInProgress = true;
            _resourceCounter.RemoveScore(BaseCost);
            unit.CreateBase();
            _units.Remove(unit);
            return;
        }
    }

    private void AssignResourceToUnit()
    {
        foreach (Unit unit in _units.Where(unit => unit.IsBusy == false))
        {
            Resource resource = _resourceContainer.FindResource(unit.transform.position);

            if (resource != null && _resourceContainer.TryReserve(resource))
                unit.AssignResource(resource);
        }
    }

    private void OnDisable()
    {
        _scanner.ResourceFound -= _resourceContainer.AddResource;
        OnReleased?.Invoke(this);
    }
}