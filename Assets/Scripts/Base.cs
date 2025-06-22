using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private ItemStorage _storage;
    [SerializeField] private Scanner _scanner;
    [SerializeField] private List<Unit> _units;

    private List<Item> _bookedItems;

    private void Awake()
    {
        _bookedItems = new List<Item>();
    }

    private void OnEnable()
    {
        _storage.Got += GetItem;
        _scanner.Scanned += SendUnits;
    }

    private void OnDisable()
    {
        _storage.Got -= GetItem;
        _scanner.Scanned -= SendUnits;
    }

    private void GetItem(int _, Item item)
    {
        _bookedItems.Remove(item);
    }

    private void SendUnits(List<Item> items)
    {
        foreach(Item bookedItem in _bookedItems)
            items.Remove(bookedItem);

        foreach(Unit unit in _units)
        {
            if(unit.Sended == false)
            {
                foreach(Item item in items)
                {
                    unit.StartGoToItem(item);
                    items.Remove(item);
                    _bookedItems.Add(item);
                    break;
                }
            }
        }
    }
}
