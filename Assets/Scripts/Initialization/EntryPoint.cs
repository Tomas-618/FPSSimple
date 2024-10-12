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
    private ChaserController _chaserController;
    private UpdateService _updateService;

    [Inject]
    private async void Construct(MainCameraFactory cameraFactory, PlayerFactory playerFactory,
        PlayerFollowCameraFactory followCameraFactory, ChaserFactory chaserFactory, MainCameraProvider cameraProvider,
        PlayerModelConfigProvider playerModelProvider, MovementConfigProvider movementProvider, BotsConfigProvider botsProvider,
        PlayerViewPrefabProvider playerViewPrefabProvider, PlayerFollowCameraProvider followCameraProvider,
        ChaserModelConfigProvider chaserModelProvider, ChaserViewPrefabProvider chaserViewPrefabProvider,
        UpdateService updateService)
    {
        if (cameraFactory == null)
            throw new ArgumentNullException(nameof(cameraFactory));

        if (playerFactory == null)
            throw new ArgumentNullException(nameof(playerFactory));

        if (followCameraFactory == null)
            throw new ArgumentNullException(nameof(followCameraFactory));

        if (chaserFactory == null)
            throw new ArgumentNullException(nameof(chaserFactory));

        if (cameraProvider == null)
            throw new ArgumentNullException(nameof(cameraProvider));

        if (playerModelProvider == null)
            throw new ArgumentNullException(nameof(playerModelProvider));

        if (movementProvider == null)
            throw new ArgumentNullException(nameof(movementProvider));

        if (botsProvider == null)
            throw new ArgumentNullException(nameof(botsProvider));

        if (playerViewPrefabProvider == null)
            throw new ArgumentNullException(nameof(playerViewPrefabProvider));

        if (followCameraProvider == null)
            throw new ArgumentNullException(nameof(followCameraProvider));

        if (chaserModelProvider == null)
            throw new ArgumentNullException(nameof(chaserModelProvider));

        if (chaserViewPrefabProvider == null)
            throw new ArgumentNullException(nameof(chaserViewPrefabProvider));

        _updateService = updateService ?? throw new ArgumentNullException(nameof(updateService));

        await Task.WhenAll(movementProvider.LoadAsync(), playerModelProvider.LoadAsync(), botsProvider.LoadAsync(),
            playerViewPrefabProvider.LoadAsync(), cameraProvider.LoadAsync(), followCameraProvider.LoadAsync(),
            chaserModelProvider.LoadAsync(), chaserViewPrefabProvider.LoadAsync());

        cameraFactory.Create();
        _playerController = playerFactory.Create();
        followCameraFactory.Create();

        _chaserController = chaserFactory.Create();

        Cursor.lockState = CursorLockMode.Locked;
        DontDestroyOnLoad(this);
    }

    private void Update() =>
        _updateService.OnUpdate();

    private void FixedUpdate() =>
        _updateService.OnFixedUpdate();

    private void LateUpdate() =>
        _updateService.OnLateUpdate();
}
