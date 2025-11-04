using UnityEngine;

public interface IBuilder
{
    public Vector3 FlagPosition { get; }
    public bool IsCreating { get; set; }
    public void Init(IUnit unit, IConstructionService constructionService, IFlagHandler flagHandler);
    public void OnCreate();
    public void Create(Vector3 flag);
}