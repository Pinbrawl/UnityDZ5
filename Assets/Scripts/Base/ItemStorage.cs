using System;
using UnityEngine;

public class ItemStorage : MonoBehaviour
{
    [SerializeField] private Base _base;

    private int _count;

    public event Action<Item> Got;
    public event Action<int> CountChanged;

    private void Awake()
    {
        _count = 0;
    }

    private void OnEnable()
    {
        _base.Purchased += Buy;
    }

    private void OnDisable()
    {
        _base.Purchased -= Buy;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PickUpper>(out PickUpper pickUpper))
        {
            Item item = pickUpper.GiveItem();

            if(item != null)
            {
                Got?.Invoke(item);
                CountChanged?.Invoke(++_count);
                item.Release();
            }
        }
    }

    private void Buy(int price)
    {
        _count -= price;
        CountChanged?.Invoke(_count);
    }
}
