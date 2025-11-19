using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class UnitAnimator : MonoBehaviour
{
    [SerializeField] private Unit _unit;
    [SerializeField] private string _emotion1;
    [SerializeField] private int _emotionsChance;
    [SerializeField] private float _interval;

    private static readonly int s_IsRun = Animator.StringToHash("IsRun");

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        StartCoroutine(CheckChance());
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
        _animator.SetBool(s_IsRun, isRun);
    }

    private IEnumerator CheckChance()
    {
        var waitTime = new WaitForSecondsRealtime(_interval);

        while (enabled)
        {
            yield return waitTime;

            if (_animator.GetBool(s_IsRun) == false)
                if (Random.Range(0, 100) < _emotionsChance)
                    _animator.Play(_emotion1);
        }
    }
}
