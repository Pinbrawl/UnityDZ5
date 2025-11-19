using UnityEngine;

public class FlagAnimator : MonoBehaviour
{
    [SerializeField] private FlagManager _flagManager;
    
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetBool("IsStand", _flagManager.IsStand);
        _animator.SetBool("CanPut", _flagManager.CanPut);
        _animator.SetBool("IsRaised", _flagManager.IsRaised);
    }
}
