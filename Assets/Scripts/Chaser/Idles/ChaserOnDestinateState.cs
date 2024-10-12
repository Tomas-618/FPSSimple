using System;
using UnityEngine;
using BasicStateMachine;
using Services;

namespace States.Chaser
{
    public class ChaserOnDestinateState : State
    {
        private readonly ChaserModel _model;
        private readonly ChaserView _view;
        private readonly TargetOperationsService _targetOperationsService;

        public ChaserOnDestinateState(ChaserModel model, ChaserView view, TargetOperationsService targetOperationsService)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _view = view != null ? view : throw new ArgumentNullException(nameof(view));
            _targetOperationsService = targetOperationsService ?? throw new ArgumentNullException(nameof(targetOperationsService));
        }

        protected override void OnEnter() =>
            _view.MoveHorizontal(Vector3.zero);

        protected override void OnLateUpdate()
        {
            _view.LookAt(_targetOperationsService.GetDirectionOnHorizontalPlane(_view.Transform.position,
            _model.Target.position));
        }
    }
}
