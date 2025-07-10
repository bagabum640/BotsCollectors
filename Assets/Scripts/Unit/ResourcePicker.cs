using UnityEngine;

public class ResourcePicker : MonoBehaviour
{
    [SerializeField] private Transform _handPosition;

    [field: SerializeField] public bool HasResource { get; private set; } = false;

    private Resource _currentResource;

    public void PickResource(Resource resource)
    {
        HasResource = true;   

        _currentResource = resource;
        _currentResource.PickUp(_handPosition);
    }

    public void DropResource()
    {
        HasResource = false;
        _currentResource = null;
    }
}