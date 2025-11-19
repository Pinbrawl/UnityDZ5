using System;
using UnityEngine;

public class Flag : MonoBehaviour
{
    private Unit _myUnit;

    public event Action<Unit, Transform> UnitHasCome;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Unit>(out Unit unit))
        {
            if (unit == _myUnit)
            {
                UnitHasCome?.Invoke(_myUnit, transform);
                _myUnit = null;
            }
        }
    }

    public void TakeUnit(Unit unit)
    {
        _myUnit = unit;
    }
}
