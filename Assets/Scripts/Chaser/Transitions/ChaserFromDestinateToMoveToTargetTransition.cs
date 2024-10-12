using System;
using BasicStateMachine;
using Providers;
using Services;

namespace Transitions.Chaser
{
    public class ChaserFromDestinateToMoveToTargetTransition : Transition
    {
        private readonly ChaserModel _model;
        private readonly ChaserView _view;
        private readonly BotsConfigProvider _botsConfigProvider;
        private readonly TargetOperationsService _targetOperationsService;

        public ChaserFromDestinateToMoveToTargetTransition(State nextState, ChaserModel model, ChaserView view,
            BotsConfigProvider botsConfigProvider, TargetOperationsService targetOperationsService) : base(nextState)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _view = view != null ? view : throw new ArgumentNullException(nameof(view));
            _botsConfigProvider = botsConfigProvider ?? throw new ArgumentNullException(nameof(botsConfigProvider));
            _targetOperationsService = targetOperationsService ?? throw new ArgumentNullException(nameof(targetOperationsService));
        }

        public override void Update()
        {
            if (_targetOperationsService.IsNearTo(_view.Transform.position, _model.Target.position,
                _botsConfigProvider.SquareDistanceToInteract) == false)
                Open();
        }
    }
}
