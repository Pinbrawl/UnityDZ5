using UnityEngine;

public class FlagAnimator : MonoBehaviour
{
    [SerializeField] private FlagMover _flagManager;

    private const string s_IsStand = "IsStand";
    private const string s_CanPut = "CanPut";
    private const string s_IsRaised = "IsRaised";

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetBool(s_IsStand, _flagManager.IsStand);
        _animator.SetBool(s_CanPut, _flagManager.CanPut);
        _animator.SetBool(s_IsRaised, _flagManager.IsRaised);
    }
}
