using UnityEngine;

namespace PlayerConfigs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig", order = 51)]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField, Min(0)] private float _gravityValue;
        [SerializeField, Min(0)] private float _gravityFactor;
        [SerializeField, Min(0)] private float _jumpTimeout;
        [SerializeField, Min(0)] private float _terminalVelocity;

        public float GravityValue => _gravityValue;

        public float GravityFactor => _gravityFactor;

        public float JumpTimeout => _jumpTimeout;

        public float TerminalVelocity => _terminalVelocity;

        private void Reset()
        {
            _gravityValue = 15f;
            _gravityFactor = 2f;
            _jumpTimeout = 0.1f;
            _terminalVelocity = 53f;
        }
    }
}
