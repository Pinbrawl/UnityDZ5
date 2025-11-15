using TMPro;
using UnityEngine;

public class ItemsCountViewer : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private ItemStorage _storage;

    private void OnEnable()
    {
        _storage.CountChanged += Print;
    }

    private void OnDisable()
    {
        _storage.CountChanged -= Print;
    }

    private void Print(int count)
    {
        _text.text = count.ToString();
    }
}
