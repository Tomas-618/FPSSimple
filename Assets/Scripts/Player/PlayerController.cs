using System.Collections.Generic;
using UnityEngine;
using Calculators;
using Services;
using CallbackContext = UnityEngine.InputSystem.InputAction.CallbackContext;
using System;

public class PlayerController
{
    public delegate void VerticalVelocityChanger(ref float verticalVelocity);

    private readonly Dictionary<bool, VerticalVelocityChanger> _strategies;
    private readonly Dictionary<bool, VerticalVelocityChanger> _inAirStrategies;
    private readonly PlayerModel _model;
    private readonly PlayerView _view;
    private readonly RotationCalculator _rotationCalculator;
    private readonly SpeedCalculator _speedCalculator;
    private readonly HorizontalMovementCalculator _horizontalMovementCalculator;
    private readonly VerticalMovementCalculator _verticalMovementCalculator;
    private readonly RigidbodyPushingCalculator _rigidbodyPushingCalculator;
    private readonly MonoBehavioursMethodsService _monoBehavioursMethodsService;
    private readonly DetectionService _detectionService;
    private readonly PlayerInputAction _input;

    private bool _canJump;

    public PlayerController(PlayerModel model, PlayerView view, RotationCalculator rotationCalculator,
        SpeedCalculator speedCalculator, HorizontalMovementCalculator horizontalMovementCalculator,
        VerticalMovementCalculator verticalMovementCalculator, RigidbodyPushingCalculator rigidbodyPushingCalculator,
        MonoBehavioursMethodsService monoBehavioursMethodsService, DetectionService detectionService)
    {
        _model = model ?? throw new ArgumentNullException(nameof(model));
        _view = view != null ? view : throw new ArgumentNullException(nameof(view));
        _rotationCalculator = rotationCalculator ?? throw new ArgumentNullException(nameof(rotationCalculator));
        _speedCalculator = speedCalculator ?? throw new ArgumentNullException(nameof(speedCalculator));
        _horizontalMovementCalculator = horizontalMovementCalculator ??
            throw new ArgumentNullException(nameof(horizontalMovementCalculator));
        _verticalMovementCalculator = verticalMovementCalculator ??
            throw new ArgumentNullException(nameof(verticalMovementCalculator));
        _rigidbodyPushingCalculator = rigidbodyPushingCalculator ??
            throw new ArgumentNullException(nameof(rigidbodyPushingCalculator));
        _monoBehavioursMethodsService = monoBehavioursMethodsService ??
            throw new ArgumentNullException(nameof(monoBehavioursMethodsService));
        _detectionService = detectionService ?? throw new ArgumentNullException(nameof(detectionService));

        _input = new PlayerInputAction();
        _canJump = false;
        _strategies = CreateStrategies();
        _inAirStrategies = CreateInAirStrategies();

        Enable();
    }

    private void Enable()
    {
        _input.Enable();

        _input.Player.Jump.performed += OnJump;
        _monoBehavioursMethodsService.Updated += OnUpdated;
        _monoBehavioursMethodsService.LateUpdated += OnLateUpdated;
        _monoBehavioursMethodsService.ColliderHit += OnColliderHit;
    }

    private void Disable()
    {
        _input.Disable();

        _input.Player.Jump.performed -= OnJump;
        _monoBehavioursMethodsService.Updated -= OnUpdated;
        _monoBehavioursMethodsService.LateUpdated -= OnLateUpdated;
        _monoBehavioursMethodsService.ColliderHit -= OnColliderHit;
    }

    private Vector3 GetDirection()
    {
        Vector2 direction = _input.Player.Move.ReadValue<Vector2>();

        return new Vector3(direction.x, 0f, direction.y);
    }

    private Vector2 GetRotation()
    {
        Vector2 rotation = _input.Player.Rotate.ReadValue<Vector2>();

        return rotation;
    }

    private void OnJump(CallbackContext callbackContext) =>
        _canJump = true;

    private bool IsRunning() =>
        _input.Player.Sprint.IsPressed();

    private void OnUpdated()
    {
        Vector3 direction = GetDirection();

        float verticalVelocity = _model.VerticalVelocity;
        float speed = _model.Speed;

        _strategies[_detectionService.Check(_view.LegsTransform.position,
            _view.Radius, _view.GroundLayer)].Invoke(ref verticalVelocity);

        _speedCalculator.SetSpeed(ref speed, _model.WalkingSpeed,
            _model.RunningSpeed, _model.SpeedChangeRate, IsRunning());

        Vector3 horizontalVelocity = _horizontalMovementCalculator
            .GetVelocity(_view.Transform, _view.LegsTransform.position,
            direction, speed, _view.Distance, _view.GroundLayer);

        _model.SetVerticalVelocity(verticalVelocity);
        _model.SetSpeed(speed);

        _view.Move(horizontalVelocity + Vector3.up * verticalVelocity * Time.deltaTime);
    }

    private void OnLateUpdated()
    {
        Vector2 rotation = GetRotation();

        float pitch = _model.Pitch;

        _rotationCalculator.SetRotation(ref pitch, out float yaw,
            _model.Sensitivity, _model.MinRotationAngle, _model.MaxRotationAngle,
            rotation);

        _model.SetPitch(pitch);

        _view.Rotate(pitch, yaw);
    }

    private void OnColliderHit(ControllerColliderHit hit) =>
        _rigidbodyPushingCalculator.Push(hit, _model.Strength);

    private void SetOnGroundVelocity(ref float verticalVelocity)
    {
        _verticalMovementCalculator.SetVelocityOnGround(ref verticalVelocity);

        if (_canJump)
            _canJump = _verticalMovementCalculator.TryJump(ref verticalVelocity, _model.JumpHeight);
    }

    private void OnFall(ref float verticalVelocity)
    {
        _canJump = false;

        _inAirStrategies[_detectionService.Check(_view.HeadTransform.position,
            _view.Radius, _view.RoofLayer)].Invoke(ref verticalVelocity);
    }

    private void SetOnHeadHittedVelocity(ref float verticalVelocity) =>
        _verticalMovementCalculator.SetVelocityOnCollising(ref verticalVelocity);

    private void SetOnFallVelocity(ref float verticalVelocity) =>
        _verticalMovementCalculator.SetVelocityOnFalling(ref verticalVelocity);

    private Dictionary<bool, VerticalVelocityChanger> CreateStrategies()
    {
        return new Dictionary<bool, VerticalVelocityChanger>
        {
            [true] = SetOnGroundVelocity,
            [false] = OnFall
        };
    }

    private Dictionary<bool, VerticalVelocityChanger> CreateInAirStrategies()
    {
        return new Dictionary<bool, VerticalVelocityChanger>
        {
            [true] = SetOnHeadHittedVelocity,
            [false] = SetOnFallVelocity
        };
    }
}
