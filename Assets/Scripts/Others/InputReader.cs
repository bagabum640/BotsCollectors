using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const KeyCode AscendKey = KeyCode.Q;
    private const KeyCode DescendKey = KeyCode.E;
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";
    private const string MouseX = "Mouse X";
    private const string MouseY = "Mouse Y";
    private const int RightMouseButton = 1;
    private const int MiddleMouseButton = 2;
    private const int LeftMouseButton = 0;

    private bool _isAscending;
    private bool _isDescending;
    private bool _isRotating;
    private bool _isSelecting;
    private bool _isSettingFlag;

    public Vector3 MoveDirection { get; private set; }
    public Vector3 LookRotation { get; private set; }

    private void Update()
    {
        MoveDirection = new(Input.GetAxis(Horizontal),
                            Input.GetAxis(Vertical));

        LookRotation = new(Input.GetAxis(MouseX), 
                           Input.GetAxis(MouseY));

        if (Input.GetKey(DescendKey))
            _isAscending = true;

        if (Input.GetKey(AscendKey))
            _isDescending = true;

        if (Input.GetMouseButton(MiddleMouseButton))
            _isRotating = true;

        if(Input.GetMouseButton(LeftMouseButton))
            _isSelecting = true;

        if(Input.GetMouseButton(RightMouseButton))
            _isSettingFlag = true;
    }

    public bool GetIsUpMove() =>
        GetBoolAsTrigger(ref _isAscending);

    public bool GetIsDownMove() => 
        GetBoolAsTrigger(ref _isDescending);

    public bool GetIsRotation() => 
        GetBoolAsTrigger(ref _isRotating);

    public bool GetIsSelect() =>
        GetBoolAsTrigger(ref _isSelecting);

    public bool GetIsSetFlag() =>
        GetBoolAsTrigger(ref _isSettingFlag);

    private bool GetBoolAsTrigger(ref bool value)
    {
        bool localValue = value;
        value = false;
        return localValue;
    }
}