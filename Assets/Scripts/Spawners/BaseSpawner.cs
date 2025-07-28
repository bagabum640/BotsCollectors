using System.Collections;
using UnityEngine;

public class BaseSpawner : Spawner<Base>
{
    [SerializeField] private float _delay = 1f;

    private WaitForSeconds _wait;

    private void Start() =>
       _wait = new WaitForSeconds(_delay);

    public IEnumerator Spawn(Unit unit, Vector3 flagPosition)
    {
        yield return _wait;

        Base @base = Pool.Get();

        @base.transform.position = flagPosition;
        @base.Init(this);
        unit.Builder.CompleteCreate(unit.GetBase());
        unit.SetBase(@base);
        @base.AddUnit(unit);
    }
}