using System.Collections;
using UnityEngine;

public interface IBase
{
    public int AmountUnits { get; }
    public IEnumerator WorkCoroutine();
    public Vector3 GetBasePosition();
    public void AddUnit(IUnit unit);
    public bool TryGetAvailableUnit(out IUnit unit);
    public void CollectResource(Resource resource);
    public void AssignTasksToUnits();
    public void TrySpawnNewUnit();
    public void AssignResourceToUnit();
}