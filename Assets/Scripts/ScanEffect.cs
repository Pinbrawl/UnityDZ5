using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ScanEffect : MonoBehaviour
{
    [SerializeField] private Base _base;
    
    private const string ParameterName = "IsScan";

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _base.Spawned += Scan;
    }

    private void OnDisable()
    {
        _base.Spawned -= Scan;
    }

    private void Scan()
    {
        _animator.SetBool(ParameterName, true);

        StartCoroutine(StopScan());
    }

    private IEnumerator StopScan()
    {
        yield return null;

        _animator.SetBool(ParameterName, false);
    }
}
