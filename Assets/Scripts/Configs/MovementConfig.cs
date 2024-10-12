using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "MovementConfig", menuName = "Configs/MovementConfig", order = 51)]
    public class MovementConfig : ScriptableObject
    {
        [SerializeField, Min(0)] private float _checkerRadius;
        [SerializeField, Min(0)] private float _checkerDistance;
        [SerializeField, Min(0)] private float _gravityValue;
        [SerializeField, Min(0)] private float _gravityFactor;
        [SerializeField, Min(0)] private float _jumpTimeout;
        [SerializeField, Min(0)] private float _terminalVelocity;

        [field: SerializeField] public LayerMask GroundLayer { get; private set; }

        [field: SerializeField] public LayerMask RoofLayer { get; private set; }

        public float CheckerRadius => _checkerRadius;

        public float CheckerDistance => _checkerDistance;

        public float GravityValue => _gravityValue;

        public float GravityFactor => _gravityFactor;

        public float JumpTimeout => _jumpTimeout;

        public float TerminalVelocity => _terminalVelocity;

        private void Reset()
        {
            _checkerRadius = 0.5f;
            _checkerDistance = 0.7f;
            _gravityValue = 15f;
            _gravityFactor = 2f;
            _jumpTimeout = 0.1f;
            _terminalVelocity = 53f;
        }
    }
}
