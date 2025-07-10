using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const KeyCode DownKey = KeyCode.Q;
    private const KeyCode UpKey = KeyCode.E;
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";
    private const string MouseX = "Mouse X";
    private const string MouseY = "Mouse Y";
    private const int RightMouseButton = 1;

    private bool _isUp;
    private bool _isDown;
    private bool _isRotation;

    public Vector3 MoveDirection { get; private set; }
    public Vector3 LookDirection { get; private set; }

    private void Update()
    {
        MoveDirection = new(Input.GetAxis(Horizontal),
                            Input.GetAxis(Vertical));

        LookDirection = new(Input.GetAxis(MouseX), 
                            Input.GetAxis(MouseY));

        if (Input.GetKey(UpKey))
            _isUp = true;

        if (Input.GetKey(DownKey))
            _isDown = true;

        if (Input.GetMouseButton(RightMouseButton))
            _isRotation = true;
    }

    public bool GetIsUpMove() =>
        GetBoolAsTrigger(ref _isUp);

    public bool GetIsDownMove() => 
        GetBoolAsTrigger(ref _isDown);

    public bool GetIsRotation() => 
        GetBoolAsTrigger(ref _isRotation);

    private bool GetBoolAsTrigger(ref bool value)
    {
        bool localValue = value;
        value = false;
        return localValue;
    }
}