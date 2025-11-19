using System;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private ItemStorage _storage;
    [SerializeField] private Scanner _scanner;
    [SerializeField] private ItemStorage _itemStorage;
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private Flag _flag;
    [SerializeField] private FlagManager _flagManager;
    [SerializeField] private List<Unit> _units;
    [SerializeField] private int _unitPrice;
    [SerializeField] private int _basePrice;
    [SerializeField] private int _numberOfUnitsForNewBase;

    private List<Item> _bookedItems;
    private bool _priorityOnBase;

    public event Action<int> Purchased;
    public event Action<Unit, Transform> UnitHasComeToFlag;

    private void Awake()
    {
        _bookedItems = new List<Item>();
        _priorityOnBase = false;
    }

    private void OnEnable()
    {
        _flagManager.IsDelivered += ChangePriority;
        _storage.Got += GetItem;
        _scanner.Scanned += SendUnits;
        _itemStorage.CountChanged += CheckBalance;
        _flag.UnitHasCome += OnUnitHasComeToFlag;

        foreach (Unit unit in _units)
            unit.ItemPickUpped += SendToBase;
    }

    private void OnDisable()
    {
        _flagManager.IsDelivered -= ChangePriority;
        _storage.Got -= GetItem;
        _scanner.Scanned -= SendUnits;
        _flag.UnitHasCome -= OnUnitHasComeToFlag;

        foreach (Unit unit in _units)
            unit.ItemPickUpped -= SendToBase;
    }

    public void Init(Camera camera)
    {
        _flagManager.Init(camera);
    }

    public void AddUnit(Unit unit)
    {
        _units.Add(unit);
        unit.StopGoTo();
        unit.ItemPickUpped += SendToBase;
    }

    private void ChangePriority(bool isDelivered)
    {
        _priorityOnBase = isDelivered;
    }

    private void SendToBase(Unit unit)
    {
        unit.StartGoTo(transform.position);
    }

    private void CheckBalance(int count)
    {
        if ((_priorityOnBase == false || _units.Count < _numberOfUnitsForNewBase) && count >= _unitPrice)
        {
            Purchased?.Invoke(_unitPrice);

            Unit unit = _unitSpawner.Spawn();
            _units.Add(unit);
            unit.ItemPickUpped += SendToBase;
        }
        else if (count >= _basePrice)
        {
            bool sended = false;

            while (sended == false)
            {
                foreach (Unit unit in _units)
                {
                    if (unit.Sended == false)
                    {
                        unit.StartGoTo(_flag.transform.position);
                        _units.Remove(unit);
                        _flag.TakeUnit(unit);
                        unit.ItemPickUpped -= SendToBase;

                        sended = true;

                        Purchased?.Invoke(_basePrice);

                        break;
                    }
                }
            }
        }
    }

    private void GetItem(Item item)
    {
        _bookedItems.Remove(item);
    }

    private void SendUnits(List<Item> items)
    {
        foreach (Item bookedItem in _bookedItems)
            items.Remove(bookedItem);

        foreach (Unit unit in _units)
        {
            if (unit.Sended == false)
            {
                foreach (Item item in items)
                {
                    unit.StartGoToItem(item);
                    items.Remove(item);
                    _bookedItems.Add(item);
                    break;
                }
            }
        }
    }

    private void OnUnitHasComeToFlag(Unit unit, Transform transform)
    {
        UnitHasComeToFlag?.Invoke(unit, transform);
        _priorityOnBase = false;
        Destroy(_flagManager.gameObject);
    }
}
