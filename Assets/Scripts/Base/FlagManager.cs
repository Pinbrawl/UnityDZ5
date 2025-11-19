using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlagManager : MonoBehaviour
{
    [SerializeField] private Flag _flag;

    private Camera _camera;
    private Vector3 _startPosition;
    private PlayerInput _input;
    private Ray _ray;

    public event Action Spawned;
    public event Action<bool> IsDelivered;

    public bool IsRaised { get; private set; }
    public bool CanPut { get; private set; }
    public bool IsStand { get; private set; }

    private void Awake()
    {
        _camera = FindObjectOfType(typeof(Camera)).GetComponent<Camera>();
        _input = new PlayerInput();
        _startPosition = _flag.transform.position;

        _input.Player.Click.performed += OnClick;
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.Player.Click.performed += OnClick;
    }

    private void OnDisable()
    {
        _input.Player.Click.performed -= OnClick;
        _input.Disable();
    }

    private void Update()
    {
        if (IsRaised)
            Move();
    }

    public void Init(Camera camera)
    {
        _camera = camera;
    }

    public void Put()
    {
        IsRaised = false;
        IsStand = true;
        IsDelivered?.Invoke(true);
    }

    public void GoBack()
    {
        IsRaised = false;
        _flag.transform.position = _startPosition;
        IsDelivered?.Invoke(false);
    }

    private void Move()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out RaycastHit hit, Mathf.Infinity, _flag.gameObject.layer))
        {
            _flag.transform.position = hit.point;
            CanPut = hit.collider.TryGetComponent<Ground>(out _);
        }
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (IsRaised)
        {
            if (CanPut)
                Put();
            else
                GoBack();
        }
        else
        {
            RaycastHit[] hits = Physics.RaycastAll(_ray, Mathf.Infinity);

            foreach(RaycastHit hit in hits)
            {
                if(hit.collider.TryGetComponent<FlagManager>(out _))
                {
                    Spawn();
                    break;
                }
            }
        }
    }

    private void Spawn()
    {
        Spawned?.Invoke();
        IsStand = false;
        IsRaised = true;
    }
}
