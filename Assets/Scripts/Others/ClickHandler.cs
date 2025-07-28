using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    private Camera _camera;
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
                    if(_selectBase != null)
                        _selectBase.Selection.gameObject.SetActive(false);

                    _selectBase = @base;
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

    private void SetFlag(Vector3 position)
    {
        if (_selectBase != null && _selectBase.FlagIsActive == false)
        {
            _selectBase.SetFlagPosition(position);
            _selectBase.ActiveFlag();
        }
        else if (_selectBase != null && _selectBase.FlagIsActive)
        {
            _selectBase.SetFlagPosition(position);
        }
    }
}