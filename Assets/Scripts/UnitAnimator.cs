using UnityEngine;

[RequireComponent(typeof(Animator))]
public class UnitAnimator : MonoBehaviour
{
    [SerializeField] private Unit _unit;

    private const string RunParameterName = "IsRun";

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetBool(RunParameterName, _unit.Sended);
    }
}
