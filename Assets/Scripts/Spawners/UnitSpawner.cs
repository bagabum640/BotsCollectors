using System.Collections;
using UnityEngine;

public class UnitSpawner : Spawner<Unit>
{
    [SerializeField] private Base _base;
    [SerializeField] private int _startAmount = 3;
    [SerializeField] private float _spawnDelay = 0.5f;
    [SerializeField] private bool _canSpawn;

    private void Start()
    {
        if (_base != null && _canSpawn)
            StartCoroutine(SpawnUnits());
    }

    private void OnEnable()
    {
        _base.Spawned += Spawn;
    }

    private void OnDisable()
    {
        _base.Spawned -= Spawn;
    }

    private IEnumerator SpawnUnits()
    {
        WaitForSeconds wait = new(_spawnDelay);

        for (int i = 0; i < _startAmount; i++)
        {
            Spawn();

            yield return wait;
        }
    }

    public void Spawn() =>
         Pool.Get();

    protected override void SetUpObject(Unit unit)
    {
        base.SetUpObject(unit);
        unit.SetStartPosition(GetSpawnPosition());
        unit.SetBase(_base);
        _base.AddUnit(unit);
    }
}