using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CameraMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _speedBoost;

    private PlayerInput _input;
    private Vector2 _moveDirection;
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
        _moveDirection = _input.Player.Move.ReadValue<Vector2>().normalized;

        Move();
    }

    private void Move()
    {
        Vector3 offset = new Vector3(_moveDirection.x, 0, _moveDirection.y).normalized * _speed;
        _rigidbody.velocity = new Vector3(Mathf.MoveTowards(_rigidbody.velocity.x, offset.x, _speedBoost * Time.deltaTime), 0, Mathf.MoveTowards(_rigidbody.velocity.z, _rigidbody.velocity.z + offset.z, _speedBoost * Time.deltaTime));
    }
}
