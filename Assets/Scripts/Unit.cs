using System.Collections;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private PickUpper _pickUpper;
    [SerializeField] private Base _base;
    [SerializeField] private float _speed;

    private Coroutine _coroutine;

    public bool Sended { get; private set; }

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

    private void StartGoToBase()
    {
        StartGoTo(_base.transform);
    }

    private void StopGoTo()
    {
        Sended = false;

        if(_coroutine != null)
            StopCoroutine(_coroutine);
    }

    public void StartGoTo(Transform obj)
    {
        StopGoTo();

        Sended = true;

        _coroutine = StartCoroutine(GoTo(obj));
    }

    private IEnumerator GoTo(Transform obj)
    {
        while(enabled)
        {
            transform.LookAt(obj);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            Move();

            yield return null;
        }
    }

    private void Move()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * _speed);
    }
}
