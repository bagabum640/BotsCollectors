using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Resource : MonoBehaviour, IPoolable<Resource>
{
    private Rigidbody _rigidbody;

    public event Action<Resource> OnReleased;

    private void Awake() =>    
        _rigidbody = GetComponent<Rigidbody>();

    public void PickUp(Transform holder)
    {
        _rigidbody.isKinematic = true;
        transform.SetParent(holder);
        transform.localPosition = Vector3.zero;
    }

    public void Release()
    {
        OnReleased?.Invoke(this);
        _rigidbody.isKinematic = false;
        transform.SetParent(null);
    }
}