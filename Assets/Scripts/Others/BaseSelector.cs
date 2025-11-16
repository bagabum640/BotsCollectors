using UnityEngine;

public class BaseSelector : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    private Camera _camera;
    private Flag _currentFlag;
    private Base _selectBase;

    private void Awake() =>
        _camera = Camera.main;

    private void Update() =>
        HandleClick();

    private void HandleClick()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        bool hasHit = Physics.Raycast(ray, out RaycastHit hit);

        if (_inputReader.GetIsSelect())
        {
            if (hasHit)
            {
                if (hit.collider.TryGetComponent(out Base @base))
                {
                    if (_selectBase != null)
                        _selectBase.Selection.gameObject.SetActive(false);

                    _selectBase = @base;
                    _currentFlag = _selectBase.Flag;
                    _selectBase.Selection.gameObject.SetActive(true);
                }
                else
                {
                    if (_selectBase != null)
                    {
                        _selectBase.Selection.gameObject.SetActive(false);
                        _selectBase = null;
                    }
                }
            }
        }

        if (_inputReader.GetIsSetFlag())
        {
            if (_selectBase != null && hit.collider.TryGetComponent(out Terrain _))
            {
                SetFlag(hit.point);
            }
        }
    }

    public void ActiveFlag() =>
        _currentFlag.gameObject.SetActive(true);

    public void DeactiveFlag() =>
        _currentFlag.gameObject.SetActive(false);

    public void SetFlagPosition(Vector3 position) =>
        _currentFlag.transform.position = position;

    private void SetFlag(Vector3 position)
    {
        if (_selectBase != null && _selectBase.FlagIsActive == false)
        {
            SetFlagPosition(position);
            ActiveFlag();
        }
        else if (_selectBase != null && _selectBase.FlagIsActive)
        {
            SetFlagPosition(position);
        }
    }
}