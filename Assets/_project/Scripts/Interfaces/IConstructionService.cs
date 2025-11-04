using UnityEngine;

public interface IConstructionService
{
    public bool TryStartConstruction(IBase @base);
    public void CompleteBaseConstruction(IUnit unit, Vector3 position);
    public bool CanBuildNewBase(IBase @base);
}