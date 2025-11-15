using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlagSpawner : MonoBehaviour
{
    [SerializeField] private Flag _flag;

    private Camera _camera;
    private PlayerInput _input;
    private Ray _ray;

    public event Action Spawned;

    private void Awake()
    {
        _camera = FindObjectOfType(typeof(Camera)).GetComponent<Camera>();
        _input = new PlayerInput();

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

    private void OnClick(InputAction.CallbackContext context)
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] hits = Physics.RaycastAll(_ray, Mathf.Infinity);

        foreach(RaycastHit hit in hits)
        {
            if(hit.collider.TryGetComponent<FlagSpawner>(out _))
            {
                Spawn();
                break;
            }
        }
    }

    private void Spawn()
    {
        Spawned?.Invoke();
        _flag.Raise();
    }
}
