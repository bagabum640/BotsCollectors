using UnityEngine;

public class FlagHandler : MonoBehaviour, IFlagHandler
{
    [SerializeField] private Flag _flag;

    public bool FlagIsActive => _flag != null &&_flag.gameObject.activeInHierarchy;

    public Vector3 GetFlagPosition => _flag.gameObject.transform.position;

    public void SetFlag(IBase selectBase, Vector3 position)
    {
        if (selectBase != null && FlagIsActive == false)
        {
            SetFlagPosition(position);
            _flag.Active();
        }
        else if (selectBase != null && FlagIsActive)
        {
            SetFlagPosition(position);
        }
    }

    public void SetFlagPosition(Vector3 newPosition) =>
            _flag.transform.position = newPosition;
}