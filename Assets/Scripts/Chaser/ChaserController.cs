using System;
using Services;
using Providers;
using BasicStateMachine;

public class ChaserController
{
    private readonly ChaserModel _model;
    private readonly StateMachine _stateMachine;
    private readonly UpdateService _updateService;

    public ChaserController(ChaserModel model, StateMachine stateMachine, PlayerViewProvider playerViewProvider,
        UpdateService updateService)
    {
        if (playerViewProvider == null)
            throw new ArgumentNullException(nameof(playerViewProvider));

        _model = model ?? throw new ArgumentNullException(nameof(model));
        _stateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine));
        _updateService = updateService ?? throw new ArgumentNullException(nameof(updateService));

        _model.SetTarget(playerViewProvider.View.transform);

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

    private void OnFixedUpdated() =>
        _stateMachine.Update();

    private void OnLateUpdated() =>
        _stateMachine.UpdateLately();
}
