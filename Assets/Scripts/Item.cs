using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    public event Action<Item> Released;

    public void Release()
    {
        Released?.Invoke(this);
    }
}
