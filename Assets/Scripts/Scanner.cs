using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Scanner : MonoBehaviour
{
    [SerializeField] private float _scanRadius;
    [SerializeField] private float _interval;

    private List<Item> _items;

    public event Action<List<Item>> Scanned;

    private void Awake()
    {
        _items = new List<Item>();
    }

    private void Start()
    {
        StartCoroutine(DoScan());
    }

    private void Scan()
    {
        _items.Clear();

        Collider[] colliders = Physics.OverlapSphere(transform.position, _scanRadius);

        foreach (Collider collider in colliders)
            if (collider.TryGetComponent<Item>(out Item item))
                _items.Add(item);

        Scanned?.Invoke(_items);
    }

    private IEnumerator DoScan()
    {
        var waitTime = new WaitForSecondsRealtime(_interval);

        while (enabled)
        {
            yield return waitTime;

            Scan();
        }
    }
}
