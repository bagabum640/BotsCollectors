using UnityEngine;

public class ConstructionService : MonoBehaviour, IConstructionService
{
    private const int BaseCost = 1;
    private const int MinAmountUnitsForCreateNewBase = 1;

    private IBaseSpawner _baseSpawner;
    private IResourceCounter _resourceCounter;

    [Inject]
    public void Construct(IBaseSpawner baseSpawner, IResourceCounter resourceCounter)
    {
        _baseSpawner = baseSpawner;
        _resourceCounter = resourceCounter;
    }

    public bool TryStartConstruction(IBase @base)
    {
        if (CanBuildNewBase(@base) == false || @base.TryGetAvailableUnit(out IUnit unit) == false)
            return false;

        unit.CreateBase();
        return true;
    }

    public void CompleteBaseConstruction(IUnit unit, Vector3 position)
    {
        _resourceCounter.RemoveScore(BaseCost);
        StartCoroutine(_baseSpawner.Create(unit, position));
    }

    public bool CanBuildNewBase(IBase @base)
    {
        return  _resourceCounter.ResourceAmount >= BaseCost && @base.AmountUnits > MinAmountUnitsForCreateNewBase;
    }
}