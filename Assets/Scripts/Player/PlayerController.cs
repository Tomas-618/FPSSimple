using System;
using System.Collections.Generic;
using UnityEngine;
using Calculators;
using Services;
using Providers;
using CallbackContext = UnityEngine.InputSystem.InputAction.CallbackContext;

public class PlayerController
{
    public delegate void VerticalVelocityChanger(ref float verticalVelocity);

    private readonly Dictionary<bool, VerticalVelocityChanger> _strategies;
    private readonly Dictionary<bool, VerticalVelocityChanger> _inAirStrategies;
    private readonly Dictionary<bool, VerticalVelocityChanger> _jumpStrategies;
    private readonly PlayerModel _model;
    private readonly PlayerView _view;
    private readonly MovementConfigProvider _movementProvider;
    private readonly RotationCalculator _rotationCalculator;
    private readonly SpeedCalculator _speedCalculator;
    private readonly HorizontalMovementCalculator _horizontalMovementCalculator;
    private readonly VerticalMovementCalculator _verticalMovementCalculator;
    private readonly UpdateService _updateService;
    private readonly DetectionService _detectionService;
    private readonly PlayerInputAction _input;

    private float _jumpTimeoutDelta;
    private bool _canJump;

    public PlayerController(PlayerModel model, PlayerView view, MovementConfigProvider movementProvider,
        RotationCalculator rotationCalculator, SpeedCalculator speedCalculator,
        HorizontalMovementCalculator horizontalMovementCalculator, VerticalMovementCalculator verticalMovementCalculator,
        UpdateService updateService, DetectionService detectionService)
    {
        _model = model ?? throw new ArgumentNullException(nameof(model));
        _view = view != null ? view : throw new ArgumentNullException(nameof(view));
        _movementProvider = movementProvider ?? throw new ArgumentNullException(nameof(movementProvider));
        _rotationCalculator = rotationCalculator ?? throw new ArgumentNullException(nameof(rotationCalculator));
        _speedCalculator = speedCalculator ?? throw new ArgumentNullException(nameof(speedCalculator));
        _horizontalMovementCalculator = horizontalMovementCalculator ??
            throw new ArgumentNullException(nameof(horizontalMovementCalculator));
        _verticalMovementCalculator = verticalMovementCalculator ??
            throw new ArgumentNullException(nameof(verticalMovementCalculator));
        _updateService = updateService ?? throw new ArgumentNullException(nameof(updateService));
        _detectionService = detectionService ?? throw new ArgumentNullException(nameof(detectionService));

        _input = new PlayerInputAction();
        _jumpTimeoutDelta = _movementProvider.JumpTimeout;
        _canJump = false;

        _strategies = CreateStrategies();
        _inAirStrategies = CreateInAirStrategies();
        _jumpStrategies = CreateJumpStrategies();

        Enable();
    }

    private void Enable()
    {
        _input.Enable();

        _input.Player.Jump.performed += OnJumpButtonPressed;
        _updateService.Updated += OnUpdated;
        _updateService.LateUpdated += OnLateUpdated;
    }

    private void Disable()
    {
        _input.Disable();

        _input.Player.Jump.performed -= OnJumpButtonPressed;
        _updateService.Updated -= OnUpdated;
        _updateService.LateUpdated -= OnLateUpdated;
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

    private void OnJumpButtonPressed(CallbackContext callbackContext) =>
        _canJump = true;

    private void DisableJump() =>
        _canJump = false;

    private bool IsRunning() =>
        _input.Player.Sprint.IsPressed();

    private void OnUpdated()
    {
        Vector3 direction = GetDirection();

        float verticalVelocity = _model.VerticalVelocity;
        float speed = _model.Speed;

        _strategies[_detectionService.Check(_view.LegsTransform.position,
            _movementProvider.CheckerRadius, _movementProvider.GroundLayer)]
            .Invoke(ref verticalVelocity);

        _speedCalculator.SetSpeed(ref speed, _model.WalkingSpeed,
            _model.RunningSpeed, _model.SpeedChangeRate, IsRunning());

        Vector3 horizontalVelocity = _horizontalMovementCalculator
            .GetVelocity(_view.Transform, _view.LegsTransform.position, direction,
            speed, _movementProvider.CheckerDistance, _movementProvider.GroundLayer);

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

    private void SetOnGroundVelocity(ref float verticalVelocity)
    {
        _verticalMovementCalculator.SetVelocityOnCollising(ref verticalVelocity);

        if (_jumpTimeoutDelta >= 0f)
            _jumpTimeoutDelta -= Time.deltaTime;

        if (_canJump)
            _jumpStrategies[_jumpTimeoutDelta <= 0f].Invoke(ref verticalVelocity);
    }

    private void OnJump(ref float verticalVelocity)
    {
        _verticalMovementCalculator.Jump(ref verticalVelocity,
                _movementProvider.GravityFactor, _movementProvider.GravityValue, _model.JumpHeight);
    }

    private void OnFall(ref float verticalVelocity)
    {
        DisableJump();
        _jumpTimeoutDelta = _movementProvider.JumpTimeout;

        _inAirStrategies[_detectionService.Check(_view.HeadTransform.position,
            _movementProvider.CheckerRadius, _movementProvider.RoofLayer)]
            .Invoke(ref verticalVelocity);
    }

    private void SetOnHeadHittedVelocity(ref float verticalVelocity) =>
        _verticalMovementCalculator.SetVelocityOnCollising(ref verticalVelocity);

    private void SetOnFallVelocity(ref float verticalVelocity)
    {
        _verticalMovementCalculator.SetVelocityOnFalling(ref verticalVelocity,
            _movementProvider.TerminalVelocity, _movementProvider.GravityValue);
    }

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

    private Dictionary<bool, VerticalVelocityChanger> CreateJumpStrategies()
    {
        return new Dictionary<bool, VerticalVelocityChanger>
        {
            [true] = OnJump,
            [false] = (ref float verticalVelocity) => DisableJump()
        };
    }
}
