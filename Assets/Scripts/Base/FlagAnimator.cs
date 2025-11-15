using UnityEngine;

[RequireComponent(typeof(Flag))]
public class FlagAnimator : MonoBehaviour
{
    private Flag _flag;
    private Animator _animator;

    private void Awake()
    {
        _flag = GetComponent<Flag>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetBool("IsStand", _flag.IsStand);
        _animator.SetBool("CanPut", _flag.CanPut);
        _animator.SetBool("IsRaised", _flag.IsRaised);
    }
}
