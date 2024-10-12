using System;
using BasicStateMachine;
using Calculators;
using Providers;
using Services;

namespace Factories
{
    public class ChaserControllerFactory
    {
        private readonly PlayerViewProvider _playerViewProvider;
        private readonly UpdateService _updateService;

        public ChaserControllerFactory(PlayerViewProvider playerViewProvider, UpdateService updateService)
        {
            _playerViewProvider = playerViewProvider ?? throw new ArgumentNullException(nameof(playerViewProvider));
            _updateService = updateService ?? throw new ArgumentNullException(nameof(updateService));
        }

        public ChaserController Create(ChaserModel model, StateMachine stateMachine) =>
            new ChaserController(model, stateMachine, _playerViewProvider, _updateService);
    }
}
