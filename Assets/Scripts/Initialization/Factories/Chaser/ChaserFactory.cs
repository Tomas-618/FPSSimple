using System;
using BasicStateMachine;

namespace Factories
{
    public class ChaserFactory
    {
        private readonly ChaserModelFactory _modelFactory;
        private readonly ChaserViewFactory _viewFactory;
        private readonly ChaserStateMachineFactory _stateMachineFactory;
        private readonly ChaserControllerFactory _controllerFactory;

        public ChaserFactory(ChaserModelFactory modelFactory, ChaserViewFactory viewFactory,
            ChaserStateMachineFactory stateMachineFactory, ChaserControllerFactory controllerFactory)
        {
            _modelFactory = modelFactory ?? throw new ArgumentNullException(nameof(modelFactory));
            _viewFactory = viewFactory ?? throw new ArgumentNullException(nameof(viewFactory));
            _stateMachineFactory = stateMachineFactory ?? throw new ArgumentNullException(nameof(stateMachineFactory));
            _controllerFactory = controllerFactory ?? throw new ArgumentNullException(nameof(controllerFactory));
        }

        public ChaserController Create()
        {
            ChaserModel model = _modelFactory.Create();
            ChaserView view = _viewFactory.Create();
            StateMachine stateMachine = _stateMachineFactory.Create(model, view);
            ChaserController controller = _controllerFactory.Create(model, stateMachine);

            return controller;
        }
    }
}
