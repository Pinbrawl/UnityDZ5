using TMPro;
using UnityEngine;

public class ItemsCountViewer : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Base _base;

    private void OnEnable()
    {
        _base.ItemGetting += Print;
    }

    private void OnDisable()
    {
        _base.ItemGetting -= Print;
    }

    private void Print(int count)
    {
        _text.text = count.ToString();
    }
}
