using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private List<Unit> _units;
    [SerializeField] private float _scanRadius;
    [SerializeField] private float _interval;

    private List<Item> _items;
    private int _itemsCount;

    public event Action Spawned;
    public event Action<int> ItemGetting;

    private void Awake()
    {
        _items = new List<Item>();
        _itemsCount = 0;
    }

    private void Start()
    {
        StartCoroutine(DoScan());
    }

    public void GetItem()
    {
        ItemGetting?.Invoke(++_itemsCount);
    }

    private void Scan()
    {
        _items.Clear();

        Collider[] colliders = Physics.OverlapSphere(transform.position, _scanRadius);

        foreach(Collider collider in colliders)
            if(collider.TryGetComponent<Item>(out Item item))
                if(item.PickUpped == false && item.Booked == false)
                    _items.Add(item);
    }

    private void SendUnits()
    {
        foreach(Unit unit in _units)
        {
            if(unit.Sended == false)
            {
                foreach(Item item in _items)
                {
                    if(item.PickUpped == false && item.Booked == false)
                    {
                        unit.StartGoTo(item.transform);
                        _items.Remove(item);
                        item.Booked = true;
                        break;
                    }
                }
            }
        }
    }

    private IEnumerator DoScan()
    {
        var waitTime = new WaitForSecondsRealtime(_interval);

        while(enabled)
        {
            yield return waitTime;

            Scan();
            SendUnits();

            Spawned?.Invoke();
        }
    }
}
