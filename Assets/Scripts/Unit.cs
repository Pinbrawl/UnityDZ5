using System;
using System.Collections;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private PickUpper _pickUpper;
    [SerializeField] private Base _base;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;

    private Coroutine _coroutine;

    public bool Sended { get; private set; }
    public Item RequiredItem { get; private set; }

    public event Action<bool> IsRun;

    private void Awake()
    {
        Sended = false;
    }

    private void OnEnable()
    {
        _pickUpper.PickUped += StartGoToBase;
        _pickUpper.Gived += StopGoTo;
    }

    private void OnDisable()
    {
        _pickUpper.PickUped -= StartGoToBase;
        _pickUpper.Gived -= StopGoTo;
    }

    public void StartGoToItem(Item item)
    {
        RequiredItem = item;

        StartGoTo(item.transform);
    }

    private void StartGoToBase()
    {
        StartGoTo(_base.transform);
    }

    private void StopGoTo()
    {
        Sended = false;
        IsRun?.Invoke(false);

        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    public void StartGoTo(Transform obj)
    {
        StopGoTo();

        Sended = true;
        IsRun?.Invoke(true);

        _coroutine = StartCoroutine(GoTo(obj));
    }

    private IEnumerator GoTo(Transform obj)
    {
        while(enabled)
        {
            Quaternion tempRotation = transform.rotation;
            transform.LookAt(obj);
            transform.rotation = Quaternion.Euler(0, Mathf.MoveTowardsAngle(tempRotation.eulerAngles.y, transform.rotation.eulerAngles.y, _rotationSpeed * Time.deltaTime), 0);
            Move();

            yield return null;
        }
    }

    private void Move()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * _speed);
    }
}
