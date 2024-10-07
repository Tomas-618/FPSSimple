using System;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using Services;
using Factories;
using Providers;

public class EntryPoint : MonoBehaviour
{
    private PlayerController _playerController;
    private UpdateService _updateService;

    [Inject]
    private async void Construct(PlayerFactory playerFactory, PlayerModelConfigProvider playerModelProvider,
        MovementConfigProvider movementProvider, PlayerViewProvider playerViewProvider, UpdateService updateService)
    {
        if (playerModelProvider == null)
            throw new ArgumentNullException(nameof(playerModelProvider));

        if (movementProvider == null)
            throw new ArgumentNullException(nameof(movementProvider));

        if (playerViewProvider == null)
            throw new ArgumentNullException(nameof(playerViewProvider));

        _updateService = updateService ?? throw new ArgumentNullException(nameof(updateService));

        await Task.WhenAll(movementProvider.LoadAsync(), playerModelProvider.LoadAsync(), playerViewProvider.LoadAsync());

        _playerController = playerFactory.Create();

        Cursor.lockState = CursorLockMode.Locked;
        DontDestroyOnLoad(this);
    }

    private void Update() =>
        _updateService.OnUpdate();

    private void LateUpdate() =>
        _updateService.OnLateUpdate();
}
