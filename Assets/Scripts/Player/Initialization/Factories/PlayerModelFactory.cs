using System;
using PlayerConfigs;

public class PlayerModelFactory
{
    private readonly PlayerModelConfig _modelConfig;

    public PlayerModelFactory(PlayerModelConfig modelConfig) =>
        _modelConfig = modelConfig != null ? modelConfig : throw new ArgumentNullException(nameof(modelConfig));

    public PlayerModel Create()
    {
        return new PlayerModel(_modelConfig.WalkingSpeed, _modelConfig.RunningSpeed,
            _modelConfig.SpeedChangeRate, _modelConfig.Sensitivity, _modelConfig.MinRotationAngle,
            _modelConfig.MaxRotationAngle, _modelConfig.Strength, _modelConfig.JumpHeight);
    }
}
