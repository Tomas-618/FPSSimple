using System;
using BasicStateMachine;
using Providers;
using Calculators;
using Services;
using States.Chaser;
using Transitions.Chaser;

namespace Factories
{
    public class ChaserStateMachineFactory
    {
        private readonly HorizontalMovementCalculator _horizontalMovementCalculator;
        private readonly MovementConfigProvider _movementConfigProvider;
        private readonly BotsConfigProvider _botsConfigProvider;
        private readonly TargetOperationsService _targetOperationsService;

        public ChaserStateMachineFactory(HorizontalMovementCalculator horizontalMovementCalculator,
            MovementConfigProvider movementConfigProvider, BotsConfigProvider botsConfigProvider,
            TargetOperationsService targetOperationsService)
        {
            _horizontalMovementCalculator = horizontalMovementCalculator ??
                throw new ArgumentNullException(nameof(horizontalMovementCalculator));
            _movementConfigProvider = movementConfigProvider ?? throw new ArgumentNullException(nameof(movementConfigProvider));
            _botsConfigProvider = botsConfigProvider ?? throw new ArgumentNullException(nameof(botsConfigProvider));
            _targetOperationsService = targetOperationsService ?? throw new ArgumentNullException(nameof(targetOperationsService));
        }

        public StateMachine Create(ChaserModel model, ChaserView view)
        {
            ChaserIdleState idleState = new ChaserIdleState(view);
            ChaserMoveToTargetState moveToTargetState = new ChaserMoveToTargetState(model, view,
                _horizontalMovementCalculator, _movementConfigProvider, _targetOperationsService);
            ChaserOnDestinateState onDestinateState = new ChaserOnDestinateState(model, view, _targetOperationsService);

            ChaserDestinateTransition destinateTransition = new ChaserDestinateTransition(onDestinateState, model, view,
                _botsConfigProvider, _targetOperationsService);
            ChaserFromDestinateToMoveToTargetTransition fromDestinateToMoveToTargetTransition =
                new ChaserFromDestinateToMoveToTargetTransition(moveToTargetState, model, view,
                _botsConfigProvider, _targetOperationsService);
            ChaserFromIdleToMoveToTargetTransition fromIdleToMoveToTargetTransition =
                new ChaserFromIdleToMoveToTargetTransition(moveToTargetState, model);
            ChaserIdleTransition idleTransition = new ChaserIdleTransition(idleState, model);

            idleState.AddTransition(fromIdleToMoveToTargetTransition);

            moveToTargetState.AddTransition(idleTransition);
            moveToTargetState.AddTransition(destinateTransition);

            onDestinateState.AddTransition(fromDestinateToMoveToTargetTransition);
            onDestinateState.AddTransition(idleTransition);

            return new StateMachine(idleState);
        }
    }
}
