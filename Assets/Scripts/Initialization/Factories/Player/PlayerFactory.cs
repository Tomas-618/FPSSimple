using System;

namespace Factories
{
    public class PlayerFactory
    {
        private readonly PlayerModelFactory _modelFactory;
        private readonly PlayerPresenterFactory _presenterFactory;
        private readonly PlayerViewFactory _viewFactory;
        private readonly PlayerControllerFactory _controllerFactory;

        public PlayerFactory(PlayerModelFactory modelFactory, PlayerPresenterFactory presenterFactory,
            PlayerViewFactory viewFactory, PlayerControllerFactory controllerFactory)
        {
            _modelFactory = modelFactory ?? throw new ArgumentNullException(nameof(modelFactory));
            _presenterFactory = presenterFactory ?? throw new ArgumentNullException(nameof(presenterFactory));
            _viewFactory = viewFactory ?? throw new ArgumentNullException(nameof(viewFactory));
            _controllerFactory = controllerFactory ?? throw new ArgumentNullException(nameof(controllerFactory));
        }

        public PlayerController Create()
        {
            PlayerModel model = _modelFactory.Create();
            PlayerPresenter presenter = _presenterFactory.Create(model);
            PlayerView view = _viewFactory.Create(presenter);
            PlayerController controller = _controllerFactory.Create(view, model);

            return controller;
        }
    }
}
