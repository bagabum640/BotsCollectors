using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private FlagHandler _flagHandler;

    private Camera _camera;
    private ISelectable _currentSelection;

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
                _currentSelection?.Deselect();

                if (hit.collider.TryGetComponent(out ISelectable selectable))
                {
                    selectable.Select();
                    _currentSelection = selectable;
                }
                else
                {
                    _currentSelection = null;
                }
            }
        }

        if (_inputReader.GetIsSetFlag() && _currentSelection != null)
        {
            if (hit.collider.TryGetComponent(out Terrain _))
            {
                if (_currentSelection is IBase selectableBase)
                {
                    _flagHandler.SetFlag(selectableBase, hit.point);
                }
            }
        }
    }
}