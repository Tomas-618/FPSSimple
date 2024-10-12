using System;
using BasicStateMachine;
using Calculators;
using Providers;
using Services;

namespace Factories
{
    public class ChaserControllerFactory
    {
        private readonly VerticalMovementCalculator _verticalMovementCalculator;
        private readonly PlayerViewProvider _playerViewProvider;
        private readonly MovementConfigProvider _movementConfigProvider;
        private readonly DetectionService _detectionService;
        private readonly UpdateService _updateService;

        public ChaserControllerFactory(VerticalMovementCalculator verticalMovementCalculator,
            PlayerViewProvider playerViewProvider, MovementConfigProvider movementConfigProvider,
            DetectionService detectionService, UpdateService updateService)
        {
            _verticalMovementCalculator = verticalMovementCalculator ??
                throw new ArgumentNullException(nameof(verticalMovementCalculator));
            _playerViewProvider = playerViewProvider ?? throw new ArgumentNullException(nameof(playerViewProvider));
            _movementConfigProvider = movementConfigProvider ?? throw new ArgumentNullException(nameof(movementConfigProvider));
            _detectionService = detectionService ?? throw new ArgumentNullException(nameof(detectionService));
            _updateService = updateService ?? throw new ArgumentNullException(nameof(updateService));
        }

        public ChaserController Create(ChaserView view, ChaserModel model, StateMachine stateMachine)
        {
            return new ChaserController(view, model, stateMachine, _verticalMovementCalculator,
                _playerViewProvider, _movementConfigProvider, _detectionService,
                _updateService);
        }
    }
}
