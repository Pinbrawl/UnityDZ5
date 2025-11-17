using System.Collections;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _errorRatePosition;

    private Coroutine _coroutine;

    public void StopGoTo()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    public void StartGoTo(Vector3 obj)
    {
        StopGoTo();

        _coroutine = StartCoroutine(GoTo(obj));
    }

    private IEnumerator GoTo(Vector3 obj)
    {
        while (enabled)
        {
            if ((transform.position.x < obj.x + _errorRatePosition) && (transform.position.x > obj.x - _errorRatePosition) && (transform.position.y < obj.y + _errorRatePosition) && (transform.position.y > obj.y - _errorRatePosition))
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
