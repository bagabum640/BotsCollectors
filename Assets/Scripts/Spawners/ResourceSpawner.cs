using System.Collections;
using UnityEngine;

public class ResourceSpawner : Spawner<Resource>
{
    [SerializeField] private float _spawnDelay = 1f;
    [SerializeField] private Terrain _terrain;

    private Vector3 _offsetY = Vector3.up * 0.1f;

    private void Start() =>
        StartCoroutine(SpawnResources());

    private IEnumerator SpawnResources()
    {
        WaitForSeconds wait = new(_spawnDelay);

        while (enabled)
        {
            Vector3 position = GetSpawnPosition();
            float terrainHeight = _terrain.SampleHeight(position);
            position.y = terrainHeight;

            Pool.Get().transform.position = position + _offsetY;

            yield return wait;
        }
    }
}