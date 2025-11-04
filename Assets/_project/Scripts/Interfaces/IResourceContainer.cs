using UnityEngine;

public interface IResourceContainer
{
    public void AddResource(Resource resource);
    public void Remove(Resource resource);
    public Resource FindResource(Vector3 fromPosition);
    public bool TryReserve(Resource resource);
}