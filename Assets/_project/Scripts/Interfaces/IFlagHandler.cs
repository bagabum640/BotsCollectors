using UnityEngine;

public interface IFlagHandler
{
    public bool FlagIsActive { get; }
    public Vector3 GetFlagPosition { get; }
    public void SetFlag(IBase selectBase, Vector3 position);
    public void SetFlagPosition(Vector3 newPosition);
}