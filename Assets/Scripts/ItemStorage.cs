using System;
using UnityEngine;

public class ItemStorage : MonoBehaviour
{
    private int _count;

    public event Action<int, Item> Got;

    private void Awake()
    {
        _count = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PickUpper>(out PickUpper pickUpper))
        {
            Item item = pickUpper.GiveItem();

            if(item != null)
            {
                Got?.Invoke(++_count, item);
                item.Release();
            }
        }
    }
}
