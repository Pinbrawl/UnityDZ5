using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Flag : MonoBehaviour
{
    private Camera _camera;
    private Vector3 _startPosition;
    private Ray _ray;
    private PlayerInput _input;
    private Unit _myUnit;

    public event Action<bool> IsDelivered;
    public event Action<Unit, Transform> UnitHasCome;

    public bool IsRaised { get; private set; }
    public bool CanPut { get; private set; }
    public bool IsStand { get; private set; }

    private void Awake()
    {
        _input = new PlayerInput();
        _startPosition = transform.position;

        IsRaised = false;
    }

    private void Update()
    {
        if (IsRaised)
            Move();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Player.Click.performed -= OnClick;
        _input.Disable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Unit>(out Unit unit))
        {
            if (unit == _myUnit)
            {
                UnitHasCome?.Invoke(_myUnit, transform);
                _myUnit = null;
            }
        }
    }

    public void Init(Camera camera)
    {
        _camera = camera;
    }

    public void Raise()
    {
        IsStand = false;
        IsRaised = true;
        StartCoroutine(FollowOnClick());
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        IsRaised = false;
        _input.Player.Click.performed -= OnClick;

        if (CanPut)
            Put();
        else
            GoBack();
    }

    private void Move()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(_ray, out RaycastHit hit, Mathf.Infinity, gameObject.layer))
        {
            transform.position = hit.point;
            
            if(hit.collider.TryGetComponent<Ground>(out _))
                CanPut = true;
            else
                CanPut = false;
        }
    }

    private void Put()
    {
        IsStand = true;
        _input.Player.Click.performed -= OnClick;
        IsDelivered?.Invoke(true);
    }

    private void GoBack()
    {
        transform.position = _startPosition;
        IsDelivered?.Invoke(false);
    }

    private IEnumerator FollowOnClick()
    {
        yield return null;
        _input.Player.Click.performed += OnClick;
    }

    public void TakeUnit(Unit unit)
    {
        _myUnit = unit;
    }
}
