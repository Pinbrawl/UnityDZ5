using System.Collections.Generic;
using UnityEngine;

public class BaseSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _base;
    [SerializeField] private Base _firstBase;
    [SerializeField] private Camera _camera;

    private List<Base> _baseList = new List<Base>();

    private void Start()
    {
        _baseList.Add(_firstBase);
        _firstBase.UnitHasComeToFlag += Spawn;
        _firstBase.Init(_camera);
    }

    private void OnEnable()
    {
        foreach (Base @base in _baseList)
            @base.UnitHasComeToFlag += Spawn;
    }

    private void OnDisable()
    {
        foreach (Base base1 in _baseList)
            base1.UnitHasComeToFlag -= Spawn;
    }

    private void Spawn(Unit unit, Transform transform, FlagMover flagManager)
    {
        Destroy(flagManager.gameObject);
        Base newBase = Instantiate(_base, transform.position, Quaternion.identity).GetComponent<Base>();
        newBase.AddUnit(unit);
        newBase.Init(_camera);
        _baseList.Add(newBase);
        newBase.UnitHasComeToFlag += Spawn;
    }
}
