using System;
using UnityEngine;

public class PickUpper : MonoBehaviour
{
    public bool _haveItem;

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
            if(item.PickUpped == false)
            {
                _item = item;
                other.transform.position = transform.position;
                other.transform.SetParent(transform);
                item.PickUpped = true;

                _haveItem = true;

                PickUped?.Invoke();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<Base>(out Base otherBase) && _haveItem)
        {
            _item.transform.SetParent(null);
            _item.Release();

            otherBase.GetItem();

            _haveItem = false;

            Gived?.Invoke();
        }
    }
}
