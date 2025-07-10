using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private List<Resource> _resources;
    [SerializeField] private List<Unit> _units;
    [SerializeField] private Scanner _scanner;
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private float _assingnUnitDelay = 0.5f;

    private void Start()
    {
        _scanner.ResourceFound += AddResource;

        StartCoroutine(WorkCoroutine());
    }

    public void AddUnit(Unit unit) =>
        _units.Add(unit);

    public void CollectResource(Resource resource)
    {
        resource.Release();
        _resources.Remove(resource);
        _scoreCounter.AddScore();
    }

    private IEnumerator WorkCoroutine()
    {
        WaitForSeconds wait = new(_assingnUnitDelay);

        while (enabled)
        {
            if (_scanner.IsScanning == false)
            {
                _scanner.StartScan();
            }
          
            AssignResourceToUnit();

            yield return wait;
        }
    }

    private void AddResource(Resource resource)
    {
        if (_resources.Contains(resource) == false && IsResourceAssigned(resource) == false)
            _resources.Add(resource);
    }

    private void AssignResourceToUnit()
    {
        foreach (Unit unit in _units.Where(unit => unit.IsBusy == false))
        {
            Resource resource = FindResource(unit.transform.position);

            if (resource != null)
            {
                unit.AssignResource(resource);
            }
        }
    }

    private bool IsResourceAssigned(Resource resource) =>
        _units.Any(unit => unit.AssignedResource == resource);

    private Resource FindResource(Vector3 fromPosition)
    {
        return _resources
            .Where(resource => IsResourceAssigned(resource) == false)
            .OrderBy(resource => (resource.transform.position - fromPosition).sqrMagnitude)
            .FirstOrDefault();
    }

    private void OnDestroy() =>
        _scanner.ResourceFound -= AddResource;
}