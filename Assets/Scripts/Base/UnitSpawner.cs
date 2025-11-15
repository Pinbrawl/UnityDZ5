using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private Unit _unit;
    [SerializeField] private Transform _spawnPoint;

    public Unit Spawn()
    {
        return Instantiate(_unit, _spawnPoint.position, _spawnPoint.rotation);
    }
}
