using System.Collections;
using UnityEngine;

public class BaseSpawner : Spawner<Base>
{
    [SerializeField] private ConstructionService _constructionService;
    [SerializeField] private ResourceContainer _resourceContainer;
    [SerializeField] private float _delay = 1f;

    private WaitForSeconds _wait;

    private void Start() =>
       _wait = new WaitForSeconds(_delay);

    public IEnumerator Create(Unit unit, Vector3 flagPosition)
    {
        yield return _wait;

        Base @base = Pool.Get();

        @base.transform.position = flagPosition;
        @base.Init(_constructionService, _resourceContainer);  
        unit.SetBase(@base);
        @base.AddUnit(unit);
    }
}