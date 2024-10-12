using System;
using UnityEngine;
using BasicStateMachine;

namespace States.Chaser
{
    public class ChaserIdleState : State
    {
        private readonly ChaserView _view;

        public ChaserIdleState(ChaserView view) =>
            _view = view != null ? view : throw new ArgumentNullException(nameof(view));

        protected override void OnEnter() =>
            _view.MoveHorizontal(Vector3.zero);
    }
}
