using System;
using Providers;

namespace Factories
{
    public class PlayerModelFactory
    {
        private readonly PlayerModelConfigProvider _modelConfigProvider;

        public PlayerModelFactory(PlayerModelConfigProvider modelConfigProvider) =>
            _modelConfigProvider = modelConfigProvider ?? throw new ArgumentNullException(nameof(modelConfigProvider));

        public PlayerModel Create()
        {
            return new PlayerModel(_modelConfigProvider.Config.WalkingSpeed, _modelConfigProvider.Config.RunningSpeed,
                _modelConfigProvider.Config.SpeedChangeRate, _modelConfigProvider.Config.Sensitivity,
                _modelConfigProvider.Config.MinRotationAngle,
                _modelConfigProvider.Config.MaxRotationAngle, _modelConfigProvider.Config.Strength,
                _modelConfigProvider.Config.JumpHeight);
        }
    }
}
