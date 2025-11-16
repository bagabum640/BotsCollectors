using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    private static readonly int Speed = Animator.StringToHash(nameof(Speed));

    private Animator _animator;

    private void Awake() =>   
        _animator = GetComponent<Animator>();    

    public void MoveAnimation(float speed) =>   
        _animator.SetFloat(Speed, speed);    
}