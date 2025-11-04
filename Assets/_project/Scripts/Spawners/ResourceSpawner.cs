using System.Collections;
using UnityEngine;

public class ResourceSpawner : Spawner<Resource>, IResourceSpawner
{
    [SerializeField] private float _spawnDelay = 1f;

    private void Start() =>
        StartCoroutine(SpawnResources());

    public IEnumerator SpawnResources()
    {
        WaitForSeconds wait = new(_spawnDelay);

        while (enabled)
        {
            SpawnObject().gameObject.transform.position = GetSpawnPosition();

            yield return wait;
        }
    }
}