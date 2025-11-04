using UnityEngine;

public interface IUnit
{
    public Resource AssignedResource { get; }
    public Vector3 BasePosition { get; }
    public Vector3 CurrentPosition { get; }
    public float InteractDistance { get; }
     public bool IsBusy { get; }
    public void SetStartPosition(Vector3 position);
    public void SetBase(IBase @base);
    public IBase GetBase();
    public void AssignResource(Resource resource);
    public void CreateBase();
    public void DeliverResource();
    public void ClearResource();
}