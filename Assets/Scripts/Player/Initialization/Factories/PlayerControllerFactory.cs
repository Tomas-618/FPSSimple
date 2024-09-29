using System;
using Calculators;
using PlayerConfigs;
using Services;

public class PlayerControllerFactory
{
    private readonly PlayerView _view;
    private readonly PlayerConfig _config;
    private readonly PlayerModelConfig _modelConfig;
    private readonly MonoBehavioursMethodsService _monoBehavioursMethodsService;

    public PlayerControllerFactory(PlayerView view, PlayerConfig config, PlayerModelConfig modelConfig,
        MonoBehavioursMethodsService monoBehavioursMethodsService)
    {
        _view = view != null ? view : throw new ArgumentNullException(nameof(view));
        _config = config != null ? config : throw new ArgumentNullException(nameof(config));
        _modelConfig = modelConfig != null ? modelConfig : throw new ArgumentNullException(nameof(modelConfig));
        _monoBehavioursMethodsService = monoBehavioursMethodsService ??
            throw new ArgumentNullException(nameof(monoBehavioursMethodsService));
    }

    public PlayerController Create()
    {
        PlayerModel model = new PlayerModelFactory(_modelConfig).Create();
        DetectionService detectionService = new DetectionService();

        return new PlayerController(model, _view, new RotationCalculator(), new SpeedCalculator(),
            new HorizontalMovementCalculator(detectionService), new VerticalMovementCalculator(_config),
            new RigidbodyPushingCalculator(), _monoBehavioursMethodsService, detectionService);
    }
}
