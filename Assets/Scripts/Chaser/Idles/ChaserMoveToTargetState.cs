using System;
using BasicStateMachine;
using Calculators;
using Providers;
using Services;
using UnityEngine;

namespace States.Chaser
{
    public class ChaserMoveToTargetState : State
    {
        private readonly ChaserModel _model;
        private readonly ChaserView _view;
        private readonly HorizontalMovementCalculator _horizontalMovementCalculator;
        private readonly MovementConfigProvider _movementConfigProvider;
        private readonly TargetOperationsService _targetOperationsService;

        public ChaserMoveToTargetState(ChaserModel model, ChaserView view,
            HorizontalMovementCalculator horizontalMovementCalculator, MovementConfigProvider movementConfigProvider,
            TargetOperationsService targetOperationsService)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _view = view != null ? view : throw new ArgumentNullException(nameof(view));
            _horizontalMovementCalculator = horizontalMovementCalculator ?? throw new ArgumentNullException(nameof(horizontalMovementCalculator));
            _movementConfigProvider = movementConfigProvider ?? throw new ArgumentNullException(nameof(movementConfigProvider));
            _targetOperationsService = targetOperationsService ?? throw new ArgumentNullException(nameof(targetOperationsService));
        }

        protected override void OnUpdate()
        {
            Vector3 horizontalVelocity = _horizontalMovementCalculator.GetVelocity(_view.Transform, _view.LegsTransform.position,
            Vector3.forward, _model.Speed, _movementConfigProvider.CheckerDistance,
            _movementConfigProvider.GroundLayer) / Time.deltaTime;

            _view.MoveHorizontal(horizontalVelocity);
        }

        protected override void OnLateUpdate()
        {
            _view.LookAt(_targetOperationsService.GetDirectionOnHorizontalPlane(_view.Transform.position,
            _model.Target.position));
        }
    }
}
