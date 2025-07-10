using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceContainer : MonoBehaviour
{
    [SerializeField] private List<Resource> _allResources;
    [SerializeField] private List<Resource> _reservedResources;

    public void AddResource(Resource resource)
    {
        if (_allResources.Contains(resource) == false)
            _allResources.Add(resource);
    }

    public void Remove(Resource resource)
    {
        _allResources.Remove(resource);
        _reservedResources.Remove(resource);
    }

    public Resource FindResource(Vector3 fromPosition)
    {
        return _allResources
            .Where(resource => _reservedResources.Contains(resource) == false)
            .OrderBy(resource => (resource.transform.position - fromPosition).sqrMagnitude)
            .FirstOrDefault();
    }

    public bool TryReserve(Resource resource)
    {
        if (_allResources.Contains(resource) && _reservedResources.Contains(resource) == false)
        {
            _reservedResources.Add(resource);
            return true;
        }

        return false;
    }
}