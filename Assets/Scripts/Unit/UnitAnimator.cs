using UnityEngine;

[RequireComponent(typeof(Animator))]
public class UnitAnimator : MonoBehaviour
{
    [SerializeField] private Unit _unit;

    private static readonly int IsRun = Animator.StringToHash("IsRun");

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _unit.IsRun += SetRunParameter;
    }

    private void OnDisable()
    {
        _unit.IsRun -= SetRunParameter;
    }

    private void SetRunParameter(bool isRun)
    {
        _animator.SetBool(IsRun, isRun);
    }
}
