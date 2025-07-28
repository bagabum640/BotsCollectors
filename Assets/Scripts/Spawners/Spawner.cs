using UnityEngine;
using UnityEngine.Pool;

public class Spawner<TObject> : MonoBehaviour where TObject : MonoBehaviour, IPoolable<TObject>
{
    [SerializeField] private Vector3 _minPosition;
    [SerializeField] private Vector3 _maxPosition;
    [SerializeField] private TObject Prefab;
    [SerializeField] private int PoolCapacity;
    [SerializeField] private int MaxPoolSize;

    protected ObjectPool<TObject> Pool;

    private void Awake()
    {
        Pool = new ObjectPool<TObject>(
              createFunc: () => Instantiate(Prefab, transform, true),
              actionOnGet: (@object) => SetUpObject(@object),
              actionOnRelease: (@object) => ResetObject(@object),
              defaultCapacity: PoolCapacity,
              maxSize: MaxPoolSize);
    }

    public virtual void Release(TObject @object) =>
        Pool.Release(@object);

    protected virtual void ResetObject(TObject @object)
    {
        @object.OnReleased -= Release;
        @object.gameObject.SetActive(false);
    }

    protected virtual void SetUpObject(TObject @object)
    {
        @object.OnReleased += Release;
        @object.gameObject.SetActive(true);
    }

    protected Vector3 GetSpawnPosition()
    {
        Vector3 localPosition = new(Random.Range(_minPosition.x, _maxPosition.x),
                                    Random.Range(_minPosition.y, _maxPosition.y),
                                    Random.Range(_minPosition.z, _maxPosition.z));

        return transform.TransformPoint(localPosition);
    }
}