using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool PickUpped;
    public bool Booked;

    public event Action<Item> Released;

    private void Awake()
    {
        PickUpped = false;
        Booked = false;
    }

    public void Release()
    {
        Released?.Invoke(this);
    }
}
