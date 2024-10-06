using System;
using Providers;
using Calculators;
using Services;

namespace Factories
{
    public class PlayerControllerFactory
    {
        private readonly MovementConfigProvider _provider;
        private readonly UpdateService _updateService;
        private readonly DetectionService _detectionService;
        private readonly RotationCalculator _rotationCalculator;
        private readonly SpeedCalculator _speedCalculator;
        private readonly HorizontalMovementCalculator _horizontalMovementCalculator;
        private readonly VerticalMovementCalculator _verticalMovementCalculator;

        public PlayerControllerFactory(MovementConfigProvider provider, UpdateService updateService,
            DetectionService detectionService, RotationCalculator rotationCalculator, SpeedCalculator speedCalculator,
            HorizontalMovementCalculator horizontalMovementCalculator, VerticalMovementCalculator verticalMovementCalculator)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
            _updateService = updateService ?? throw new ArgumentNullException(nameof(updateService));
            _detectionService = detectionService ?? throw new ArgumentNullException(nameof(detectionService));
            _rotationCalculator = rotationCalculator ?? throw new ArgumentNullException(nameof(rotationCalculator));
            _speedCalculator = speedCalculator ?? throw new ArgumentNullException(nameof(speedCalculator));
            _horizontalMovementCalculator = horizontalMovementCalculator
                ?? throw new ArgumentNullException(nameof(horizontalMovementCalculator));
            _verticalMovementCalculator = verticalMovementCalculator
                ?? throw new ArgumentNullException(nameof(verticalMovementCalculator));
        }

        public PlayerController Create(PlayerView view, PlayerModel model)
        {
            return new PlayerController(model, view, _provider, _rotationCalculator, _speedCalculator,
                _horizontalMovementCalculator, _verticalMovementCalculator,
                _updateService, _detectionService);
        }
    }
}
