using System.Collections.Generic;
using UnityEngine;

public class UnitCastomizer : MonoBehaviour
{
    [SerializeField] private List<GameObject> _accessories;

    private void Awake()
    {
        for(int i = 0; i < _accessories.Count; i++)
            if (Random.Range(0, 2) == 1)
                _accessories[i].SetActive(true);
    }
}
