using Zenject;
using Providers;
using Factories;
using Calculators;
using Services;

public class EntryPointInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindProviders();
        BindServices();
        BindCalculators();

        BindMainCameraFactory();
        BindPlayerFactory();
        BindPlayerFollowCameraFactory();

        BindChaserFactory();
    }

    private void BindChaserFactory()
    {
        BindChaserModelFactory();
        BindChaserViewFactory();
        BindChaserStateMachineFactory();
        BindChaserControllerFactory();

        Container.Bind<ChaserFactory>().AsSingle();
    }

    private void BindChaserModelFactory() =>
        Container.Bind<ChaserModelFactory>().AsSingle();

    private void BindChaserViewFactory() =>
        Container.Bind<ChaserViewFactory>().AsSingle();

    private void BindChaserStateMachineFactory() =>
        Container.Bind<ChaserStateMachineFactory>().AsSingle();

    private void BindChaserControllerFactory() =>
        Container.Bind<ChaserControllerFactory>().AsSingle();

    private void BindMainCameraFactory() =>
        Container.Bind<MainCameraFactory>().AsSingle();

    private void BindPlayerFactory()
    {
        BindPlayerModelFactory();
        BindPlayerPresenterFactory();
        BindPlayerViewFactory();
        BindPlayerControllerFactory();

        Container.Bind<PlayerFactory>().AsSingle();
    }

    private void BindPlayerFollowCameraFactory() =>
        Container.Bind<PlayerFollowCameraFactory>().AsSingle();

    private void BindPlayerModelFactory() =>
        Container.Bind<PlayerModelFactory>().AsSingle();

    private void BindPlayerPresenterFactory() =>
        Container.Bind<PlayerPresenterFactory>().AsSingle();

    private void BindPlayerViewFactory() =>
        Container.Bind<PlayerViewFactory>().AsSingle();

    private void BindPlayerControllerFactory() =>
        Container.Bind<PlayerControllerFactory>().AsSingle();

    private void BindServices()
    {
        Container.Bind<UpdateService>().AsSingle();
        Container.Bind<DetectionService>().AsSingle();
        Container.Bind<TargetOperationsService>().AsSingle();
    }

    private void BindCalculators()
    {
        Container.Bind<RigidbodyPushingCalculator>().AsSingle();
        Container.Bind<RotationCalculator>().AsSingle();
        Container.Bind<SpeedCalculator>().AsSingle();
        Container.Bind<HorizontalMovementCalculator>().AsSingle();
        Container.Bind<VerticalMovementCalculator>().AsSingle();
    }

    private void BindProviders()
    {
        Container.Bind<MainCameraProvider>().AsSingle();
        Container.Bind<PlayerFollowCameraProvider>().AsSingle();
        Container.Bind<CameraTargetProvider>().AsSingle();
        Container.Bind<PlayerModelConfigProvider>().AsSingle();
        Container.Bind<PlayerViewPrefabProvider>().AsSingle();
        Container.Bind<PlayerViewProvider>().AsSingle();
        Container.Bind<MovementConfigProvider>().AsSingle();
        Container.Bind<BotsConfigProvider>().AsSingle();
        Container.Bind<ChaserModelConfigProvider>().AsSingle();
        Container.Bind<ChaserViewPrefabProvider>().AsSingle();
    }
}
