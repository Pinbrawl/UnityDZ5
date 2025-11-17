using System;
using System.Collections;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private PickUpper _pickUpper;
    [SerializeField] private Mover _mover;

    public event Action<bool> IsRun;
    public event Action<Unit> ItemPickUpped;

    public bool Sended { get; private set; }
    public Item RequiredItem { get; private set; }

    private void Awake()
    {
        Sended = false;
    }

    private void OnEnable()
    {
        _pickUpper.PickUped += ProcessPickUp;
        _pickUpper.Gived += StopGoTo;
    }

    private void OnDisable()
    {
        _pickUpper.PickUped -= ProcessPickUp;
        _pickUpper.Gived -= StopGoTo;
    }

    private void ProcessPickUp()
    {
        ItemPickUpped?.Invoke(this);
    }

    public void StartGoToItem(Item item)
    {
        RequiredItem = item;

        StartGoTo(item.transform.position);
    }

    public void StopGoTo()
    {
        Sended = false;
        IsRun?.Invoke(false);

        _mover.StopGoTo();
    }

    public void StartGoTo(Vector3 obj)
    {
        Sended = true;
        IsRun?.Invoke(true);

        _mover.StartGoTo(obj);
    }
}
