using System;
using System.Collections;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private PickUpper _pickUpper;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _errorRatePosition;

    private Coroutine _coroutine;

    public bool Sended { get; private set; }
    public Item RequiredItem { get; private set; }

    public event Action<bool> IsRun;
    public event Action<Unit> ItemPickUpped;

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

        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    public void StartGoTo(Vector3 obj)
    {
        StopGoTo();

        Sended = true;
        IsRun?.Invoke(true);

        _coroutine = StartCoroutine(GoTo(obj));
    }

    private IEnumerator GoTo(Vector3 obj)
    {
        while(enabled)
        {
            if((transform.position.x < obj.x + _errorRatePosition) && (transform.position.x > obj.x - _errorRatePosition) && (transform.position.y < obj.y + _errorRatePosition) && (transform.position.y > obj.y - _errorRatePosition))
            {
                StopGoTo();
            }
            else
            {
                Quaternion tempRotation = transform.rotation;
                transform.LookAt(obj);
                transform.rotation = Quaternion.Euler(0, Mathf.MoveTowardsAngle(tempRotation.eulerAngles.y, transform.rotation.eulerAngles.y, _rotationSpeed * Time.deltaTime), 0);
                Move();
            }

            yield return null;
        }
    }

    private void Move()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * _speed);
    }
}
