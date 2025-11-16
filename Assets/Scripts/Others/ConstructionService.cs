using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConstructionService : MonoBehaviour
{
    private const int BaseCost = 1;
    private const int MinAmountUnitsForCreateNewBase = 1;

    [SerializeField] private BaseSpawner _baseSpawner;
    [SerializeField] private BaseSelector _baseSelector;

    private Base _currentBase;

    private bool _isBaseConstructionInProgress = false;

    public void ResetConstructionState()
    {
        _isBaseConstructionInProgress = false;
    }

    public bool CanBuildNewBase(Base @base)
    {
        if (_isBaseConstructionInProgress == false && (@base.ResourceCounter.ResourceAmount >= BaseCost && @base.AmountUnits > MinAmountUnitsForCreateNewBase))
        {
            _currentBase = @base;
            return true;
        }

        return false;
    }

    public void CreateNewBase(List<Unit> units, Vector3 flagPosition)
    {
        foreach (Unit unit in units.Where(unit => unit.IsBusy == false))
        {
            _isBaseConstructionInProgress = true;
            _currentBase.ResourceCounter.RemoveScore(BaseCost);
            StartCoroutine(_baseSpawner.Create(unit, flagPosition));
            _baseSelector.DeactiveFlag();
            units.Remove(unit);
            return;
        }
    }
}