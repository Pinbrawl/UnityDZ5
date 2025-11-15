using UnityEngine;

public class CameraZoomer : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _speedBoost;
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _minDistance;
    [SerializeField] private float _zOffset;

    private PlayerInput _input;
    private float _moveDirection;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _input = new PlayerInput();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void Update()
    {
        _moveDirection = (float)_input.Player.Zoom.ReadValue<Vector2>().y;

        Move();
    }

    private void Move()
    {
        Vector3 offset = -new Vector3(0, _moveDirection, 0).normalized * _speed;
        _rigidbody.velocity = new Vector3(0, Mathf.MoveTowards(_rigidbody.velocity.y, _rigidbody.velocity.y + offset.y, _speedBoost * Time.deltaTime), 0);

        transform.localPosition = new Vector3(0 , Mathf.Clamp(transform.localPosition.y, _minDistance, _maxDistance), _zOffset);
        transform.LookAt(transform.parent);
    }
}
