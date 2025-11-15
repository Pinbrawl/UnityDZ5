using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ItemGetEffect : MonoBehaviour
{
    [SerializeField] private ItemStorage _itemStorage;

    private ParticleSystem _particle;

    private void Awake()
    {
        _particle = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        _itemStorage.Got += StartEffect;
    }

    private void OnDisable()
    {
        _itemStorage.Got -= StartEffect;
    }

    private void StartEffect(Item __)
    {
        _particle.Play();
    }
}
