using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceContainer : MonoBehaviour
{
    [SerializeField] private List<Resource> _resources;
    [SerializeField] private List<Resource> _reservedResources;

    public void AddResource(Resource resource)
    {
        if (_resources.Contains(resource) == false)
            _resources.Add(resource);
    }

    public void Remove(Resource resource)
    {
        _resources.Remove(resource);
        _reservedResources.Remove(resource);
    }

    public Resource FindResource(Vector3 fromPosition)
    {
        return _resources
            .Where(resource => _reservedResources.Contains(resource) == false)
            .OrderBy(resource => (resource.transform.position - fromPosition).sqrMagnitude)
            .FirstOrDefault();
    }

    public bool TryReserve(Resource resource)
    {
        if (_resources.Contains(resource) && _reservedResources.Contains(resource) == false)
        {
            _reservedResources.Add(resource);
            return true;
        }
        return false;
    }
}