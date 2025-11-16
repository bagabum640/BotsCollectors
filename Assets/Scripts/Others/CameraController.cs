using UnityEngine;

public class CameraController : MonoBehaviour
{
    private const float RightAngle = 90;
    private const float ZeroRotation = 0f;

    [SerializeField] private InputReader _inputReader;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;

    private float _rotateX;
    private float _rotateY;

    private void Update()
    {
        HandleRotation();
        HandleMovement();
    }

    private void HandleRotation()
    {
        if (_inputReader.GetIsRotation())
        {
            Vector2 look = _inputReader.LookRotation;
            _rotateX += look.x * _rotationSpeed;
            _rotateY -= look.y * _rotationSpeed;
            _rotateY = Mathf.Clamp(_rotateY, -RightAngle, RightAngle);

            transform.localRotation = Quaternion.Euler(_rotateY, _rotateX, ZeroRotation);
        }
    }

    private void HandleMovement()
    {
        Vector3 move = _inputReader.MoveDirection;
        Vector3 movement = (transform.forward * move.y + transform.right * move.x) * _moveSpeed;

        if (_inputReader.GetIsUpMove()) 
            movement += Vector3.up * _moveSpeed;

        if (_inputReader.GetIsDownMove()) 
            movement += Vector3.down * _moveSpeed;

        transform.Translate(movement * Time.deltaTime, Space.World);
    }
}