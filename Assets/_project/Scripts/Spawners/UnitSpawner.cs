using System.Collections;
using UnityEngine;

public class UnitSpawner : Spawner<Unit>, IUnitSpawner
{
    [SerializeField] private DIContainer _container;
    [SerializeField] private int _startAmount = 3;
    [SerializeField] private float _spawnDelay = 0.5f;
    [SerializeField] private bool _canSpawn;

    private IBase _base;
    private IConstructionService _constructionService;
    private IFlagHandler _flagHandler;

    [Inject]
    public void Construct(IBase @base,
                          IConstructionService constructionService,
                          IFlagHandler flagHandler)
    {
        _base = @base;
        _constructionService = constructionService;
        _flagHandler = flagHandler;
    }

    public IEnumerator SpawnUnits()
    {
        if (_base != null && _canSpawn)
        {
            WaitForSeconds wait = new(_spawnDelay);

            for (int i = 0; i < _startAmount; i++)
            {
                SpawnObject();

                yield return wait;
            }
        }
    }

    public void CreateUnit() =>
        SpawnObject();

    protected override void SetUpObject(Unit unit)
    {
        base.SetUpObject(unit);
        unit.Init(_base, (ConstructionService)_constructionService, _flagHandler);
        unit.SetBase(_base);
        unit.SetStartPosition(GetSpawnPosition());
        _base.AddUnit(unit);
    }
}