using System;
using BasicStateMachine;

namespace Transitions.Chaser
{
    public class ChaserFromIdleToMoveToTargetTransition : Transition
    {
        private readonly ChaserModel _model;

        public ChaserFromIdleToMoveToTargetTransition(State nextState, ChaserModel model) : base(nextState) =>
            _model = model ?? throw new ArgumentNullException(nameof(model));

        public override void Update()
        {
            if (_model.Target != null)
                Open();
        }
    }
}
