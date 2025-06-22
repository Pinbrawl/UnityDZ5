using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ScanEffect : MonoBehaviour
{
    [SerializeField] private Scanner _scanner;
    
    private const string ParameterName = "IsScan";

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _scanner.Scanned += Scan;
    }

    private void OnDisable()
    {
        _scanner.Scanned -= Scan;
    }

    private void Scan(List<Item> _)
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
