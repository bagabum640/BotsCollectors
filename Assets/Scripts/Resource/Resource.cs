using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Resource : MonoBehaviour, IPoolable<Resource>
{
    private Rigidbody _rigidbody;

    public event Action<Resource> OnReleased;

    public bool OnGround { get; private set; } = false;

    private void Awake() =>    
        _rigidbody = GetComponent<Rigidbody>();

    private void OnCollisionEnter(Collision collision)
    { 
        if(collision.collider.TryGetComponent(out Terrain _))
            OnGround = true;
    }

    public void PickUp(Transform holder)
    {
        _rigidbody.isKinematic = true;
        transform.SetParent(holder);
        transform.localPosition = Vector3.zero;
    }

    public void Release()
    {
        OnGround = false;
        OnReleased?.Invoke(this);
        _rigidbody.isKinematic = false;
        transform.SetParent(null);
    }
}