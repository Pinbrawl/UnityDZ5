using System;
using UnityEngine;

public class PickUpper : MonoBehaviour
{
    [SerializeField] private Unit _unit;

    private bool _haveItem;
    private Item _item;

    public event Action PickUped;
    public event Action Gived;

    private void Awake()
    {
        _haveItem = false;
        _item = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Item>(out Item item) && _haveItem == false)
        {
            if(item == _unit.RequiredItem)
            {
                _item = item;
                other.transform.position = transform.position;
                other.transform.SetParent(transform);

                _haveItem = true;

                PickUped?.Invoke();
            }
        }
    }

    public Item GiveItem()
    {
        Item item = _item;

        if(item != null)
        {
            _item.transform.SetParent(null);
            _haveItem = false;
            _item = null;

            Gived?.Invoke();
        }

        return item;
    }
}
