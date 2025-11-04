using System.Collections;
using UnityEngine;

public class BaseSpawner : Spawner<Base>, IBaseSpawner
{
    [SerializeField] private DIContainer _container;
    [SerializeField] private float _delay = 1f;

    private WaitForSeconds _wait;

    private void Start() =>
       _wait = new WaitForSeconds(_delay);

    public IEnumerator Create(IUnit unit, Vector3 flagPosition)
    {
        yield return _wait;

        Base @base = SpawnObject();

        _container.Inject(@base);
        @base.transform.position = flagPosition;
        unit.SetBase(@base);
        @base.AddUnit(unit);
    }
}