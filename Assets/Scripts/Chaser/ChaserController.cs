using System;
using System.Collections.Generic;
using Services;
using Calculators;
using Providers;
using BasicStateMachine;

public class ChaserController
{
    private readonly Dictionary<bool, VerticalVelocityChanger> _strategies;
    private readonly ChaserView _view;
    private readonly ChaserModel _model;
    private readonly StateMachine _stateMachine;
    private readonly VerticalMovementCalculator _verticalMovementCalculator;
    private readonly MovementConfigProvider _movementConfigProvider;
    private readonly DetectionService _detectionService;
    private readonly UpdateService _updateService;

    public ChaserController(ChaserView view, ChaserModel model, StateMachine stateMachine,
        VerticalMovementCalculator verticalMovementCalculator,
        PlayerViewProvider playerViewProvider, MovementConfigProvider movementConfigProvider,
        DetectionService detectionService, UpdateService updateService)
    {
        if (playerViewProvider == null)
            throw new ArgumentNullException(nameof(playerViewProvider));

        _view = view != null ? view : throw new ArgumentNullException(nameof(view));
        _model = model ?? throw new ArgumentNullException(nameof(model));
        _stateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine));
        _verticalMovementCalculator = verticalMovementCalculator ??
            throw new ArgumentNullException(nameof(verticalMovementCalculator));
        _movementConfigProvider = movementConfigProvider ?? throw new ArgumentNullException(nameof(movementConfigProvider));
        _detectionService = detectionService ?? throw new ArgumentNullException(nameof(detectionService));
        _updateService = updateService ?? throw new ArgumentNullException(nameof(updateService));

        _model.SetTarget(playerViewProvider.View.transform);
        _strategies = CreateStrategies();

        Enable();
    }

    private void Enable()
    {
        _updateService.FixedUpdated += OnFixedUpdated;
        _updateService.LateUpdated += OnLateUpdated;
    }

    private void Disable()
    {
        _updateService.FixedUpdated -= OnFixedUpdated;
        _updateService.LateUpdated -= OnLateUpdated;
    }

    private void OnFixedUpdated()
    {
        float verticalVelocity = _model.VerticalVelocity;

        _strategies[_detectionService.Check(_view.LegsTransform.position,
            _movementConfigProvider.CheckerRadius,
            _movementConfigProvider.GroundLayer)].Invoke(ref verticalVelocity);

        _stateMachine.Update();
        _model.SetVerticalVelocity(verticalVelocity);

        _view.MoveVertical(verticalVelocity);
    }

    private void OnLateUpdated() =>
        _stateMachine.UpdateLately();

    private void OnGround(ref float verticalVelocity) =>
        _verticalMovementCalculator.SetVelocityOnCollising(ref verticalVelocity);

    private void OnFall(ref float verticalVelocity)
    {
        _verticalMovementCalculator.SetVelocityOnFalling(ref verticalVelocity,
            _movementConfigProvider.TerminalVelocity, _movementConfigProvider.GravityValue);
    }

    private Dictionary<bool, VerticalVelocityChanger> CreateStrategies()
    {
        return new Dictionary<bool, VerticalVelocityChanger>
        {
            [true] = OnGround,
            [false] = OnFall
        };
    }
}
