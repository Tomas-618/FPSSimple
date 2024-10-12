using BasicStateMachine;
using System;

namespace Transitions.Chaser
{
    public class ChaserIdleTransition : Transition
    {
        private readonly ChaserModel _model;

        public ChaserIdleTransition(State nextState, ChaserModel model) : base(nextState) =>
            _model = model ?? throw new ArgumentNullException(nameof(model));

        public override void Update()
        {
            if (_model.Target == null)
                Open();
        }
    }
}
