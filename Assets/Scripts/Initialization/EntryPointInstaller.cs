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

        BindPlayerFactory();
    }

    private void BindPlayerFactory()
    {
        BindPlayerModelFactory();
        BindPlayerPresenterFactory();
        BindPlayerViewFactory();
        BindPlayerControllerFactory();

        Container.Bind<PlayerFactory>().AsSingle();
    }

    private void BindPlayerModelFactory() =>
        Container.Bind<PlayerModelFactory>().AsSingle();

    private void BindPlayerPresenterFactory() =>
        Container.Bind<PlayerPresenterFactory>().AsSingle();

    private void BindPlayerViewFactory() =>
        Container.Bind<PlayerViewFactory>().AsSingle();

    private void BindPlayerControllerFactory() =>
        Container.Bind<PlayerController>().AsSingle();

    private void BindServices()
    {
        Container.Bind<UpdateService>().AsSingle();
        Container.Bind<DetectionService>().AsSingle();
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
        Container.Bind<PlayerModelConfigProvider>().AsSingle();
        Container.Bind<PlayerViewProvider>().AsSingle();
        Container.Bind<MovementConfigProvider>().AsSingle();
    }
}
