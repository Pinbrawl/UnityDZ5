using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private Item _item;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;
    [SerializeField] private int _interval;
    [SerializeField] private Vector2 _baseScale;

    private ObjectPool<Item> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Item>(
            createFunc: () => InstantiateObj(),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => ActionOnRelease(obj),
            actionOnDestroy: (obj) => DestroyObj(obj),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private Item InstantiateObj()
    {
        Item item = Instantiate(_item);
        item.Released += ReleaseItem;

        return item;
    }

    private void ReleaseItem(Item item)
    {
        _pool.Release(item);
    }

    private void ActionOnGet(Item obj)
    {
        obj.gameObject.SetActive(true);
        obj.transform.position = GetRandomPosition();
        obj.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void ActionOnRelease(Item obj)
    {
        obj.gameObject.SetActive(false);
        obj.PickUpped = false;
        obj.Booked = false;
    }

    private void DestroyObj(Item obj)
    {
        obj.Released -= ReleaseItem;
        Destroy(obj.gameObject);
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 randomPosition = new Vector3(Random.Range(-transform.localScale.x, transform.localScale.x), transform.position.y, Random.Range(-transform.localScale.z, transform.localScale.z));

        while(randomPosition.x < _baseScale.x && randomPosition.x > -_baseScale.x && randomPosition.y < _baseScale.y && randomPosition.y > -_baseScale.y)
            randomPosition = new Vector3(Random.Range(-transform.localScale.x, transform.localScale.x), transform.position.y, Random.Range(-transform.localScale.z, transform.localScale.z));

        return randomPosition;
    }

    private IEnumerator Spawn()
    {
        WaitForSecondsRealtime waitTime = new WaitForSecondsRealtime(_interval);

        while(enabled)
        {
            yield return waitTime;

            _pool.Get();
        }
    }
}
