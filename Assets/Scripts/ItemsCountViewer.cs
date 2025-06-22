using TMPro;
using UnityEngine;

public class ItemsCountViewer : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private ItemStorage _storage;

    private void OnEnable()
    {
        _storage.Got += Print;
    }

    private void OnDisable()
    {
        _storage.Got -= Print;
    }

    private void Print(int count, Item _)
    {
        _text.text = count.ToString();
    }
}
