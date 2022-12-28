using Grenades.Inputs;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody body;
    [SerializeField] private float _movementSpeed = 3.0f;
    [SerializeField] private float _verticalMouseSensetivity = 3.0f;
    [SerializeField] private float _horizontalMouseSensetivity = 1.5f;
    [SerializeField] private float _throwSpeed = 3.0f;
    [SerializeField] private GameObject _contactGrenade;
    [SerializeField] private GameObject _delayGrenade;

    private CameraWrapper _camera;
    private Vector3 _movementDirection = default;
    private Quaternion _cameraRotation = default;

    private GrenadesInputAction inputActions;
    private InputAction moveAction;
    private InputAction lookAction;

    private void Awake()
    {
        SubsribeActions();
        body = GetComponent<Rigidbody>();
        _camera = new CameraWrapper(Camera.main);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible= false;
    }

    private void Update()
    {
        float y = _cameraRotation.eulerAngles.y + lookAction.ReadValue<Vector2>().x * _horizontalMouseSensetivity;
        float x = _cameraRotation.eulerAngles.x - lookAction.ReadValue<Vector2>().y * _verticalMouseSensetivity;
        _cameraRotation = Quaternion.Euler(x, y, 0);
        
        _camera.SetRotation(_cameraRotation);
    }

    private void FixedUpdate()
    {
        _movementDirection.x = moveAction.ReadValue<Vector2>().x;
        _movementDirection.y = moveAction.ReadValue<Vector2>().y;
        _movementDirection = _camera.Forward * _movementDirection.y + _camera.Right * _movementDirection.x;
        _movementDirection *= _movementSpeed;
        _movementDirection.y = body.velocity.y;

        body.velocity = _movementDirection;
    }

    private void SubsribeActions()
    {
        inputActions = new GrenadesInputAction();
        moveAction = inputActions.Player.Move;
        lookAction = inputActions.Player.Look;
        inputActions.Player.FireLeft.performed += OnFireLeft;
        inputActions.Player.FireRight.performed += OnFireRight;
    }

    private void OnFireLeft(InputAction.CallbackContext context)
    {
        ThrowGrenade(Instantiate(_contactGrenade, _camera.Position + _camera.Forward, Quaternion.identity));
    }

    private void OnFireRight(InputAction.CallbackContext obj)
    {
        ThrowGrenade(Instantiate(_delayGrenade, _camera.Position + _camera.Forward, Quaternion.identity));
    }

    private void ThrowGrenade(GameObject grenade)
    {
        Rigidbody grenadeBody = grenade.GetComponentInChildren<Rigidbody>();
        if (grenadeBody != null)
        {
            grenadeBody.velocity = _camera.PureForward * _throwSpeed;
        }
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
}
