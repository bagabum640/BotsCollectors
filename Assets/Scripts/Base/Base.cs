using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private List<Unit> _units;
    [SerializeField] private Scanner _scanner;
    [SerializeField] private ResourceContainer _resourceContainer;
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private float _assingnUnitDelay = 0.5f;

    private void Start()
    {
        _scanner.ResourceFound += _resourceContainer.AddResource;

        StartCoroutine(WorkCoroutine());
    }

    public void AddUnit(Unit unit) =>
        _units.Add(unit);

    public void CollectResource(Resource resource)
    {
        resource.Release();
        _resourceContainer.Remove(resource);
        _scoreCounter.AddScore();
    }

    private IEnumerator WorkCoroutine()
    {
        WaitForSeconds wait = new(_assingnUnitDelay);

        while (enabled)
        {
            if (_scanner.IsScanning == false)
                _scanner.StartScan();

            AssignResourceToUnit();

            yield return wait;
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

    private void OnDestroy() =>
        _scanner.ResourceFound -= _resourceContainer.AddResource;
}