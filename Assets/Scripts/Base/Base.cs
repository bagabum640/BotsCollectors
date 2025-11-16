using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Base : MonoBehaviour, IPoolable<Base>
{
    private const int MaxAmoutUnit = 6;
    private const int UnitCost = 3;

    [SerializeField] private List<Unit> _units;
    [SerializeField] private Scanner _scanner;
    [SerializeField] private ConstructionService _constructionService;
    [SerializeField] private ResourceContainer _resourceContainer;
    [SerializeField] private ResourceCounter _resourceCounter;
    [SerializeField] private SelectionIndicator _selection;
    [SerializeField] private ScoreView _scoreView;
    [SerializeField] private float _assingnUnitDelay = 0.5f;

    public event Action<Base> OnReleased;
    public event Action Spawned;

    [field: SerializeField] public Flag Flag { get; private set; }
    public SelectionIndicator Selection => _selection;
    public int AmountUnits => _units.Count;
    public bool FlagIsActive => Flag.gameObject.activeInHierarchy;

    public void Init(ConstructionService constructionService, ResourceCounter resourceCounter)
    {
        _constructionService = constructionService;
        _resourceCounter = resourceCounter;

        _scoreView.Init(_resourceCounter);
    }

    private void Start()
    {
        _resourceContainer.Init(this);
        _scanner.ResourceFound += _resourceContainer.AddResource;

        _constructionService.ResetConstructionState();
        Flag.gameObject.SetActive(false);

        StartCoroutine(WorkCoroutine());
    }

    public void AddUnit(Unit unit) =>
        _units.Add(unit);

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
        if (FlagIsActive && _constructionService.CanBuildNewBase(this))
            _constructionService.CreateNewBase(_units, Flag.transform.position);
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
                Spawned?.Invoke();
            }
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