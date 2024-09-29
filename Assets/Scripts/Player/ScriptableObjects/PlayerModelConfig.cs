using UnityEngine;

namespace PlayerConfigs
{
    [CreateAssetMenu(fileName = "PlayerModelConfig", menuName = "Configs/PlayerModelConfig", order = 52)]
    public class PlayerModelConfig : ScriptableObject
    {
        [SerializeField, Min(0)] private float _walkingSpeed;
        [SerializeField, Min(0)] private float _runningSpeed;
        [SerializeField, Min(0)] private float _speedChangeRate;
        [SerializeField, Min(0)] private float _sensitivity;
        [SerializeField, Min(0)] private float _strength;
        [SerializeField, Min(0)] private float _jumpHeight;

        [SerializeField] private float _minRotationAngle;
        [SerializeField] private float _maxRotationAngle;

        public float WalkingSpeed => _walkingSpeed;

        public float RunningSpeed => _runningSpeed;

        public float SpeedChangeRate => _speedChangeRate;

        public float Sensitivity => _sensitivity;

        public float Strength => _strength;

        public float JumpHeight => _jumpHeight;

        public float MinRotationAngle => _minRotationAngle;

        public float MaxRotationAngle => _maxRotationAngle;

        private void OnValidate()
        {
            if (_walkingSpeed >= _runningSpeed)
                _walkingSpeed = _runningSpeed - 1;

            if (_minRotationAngle >= _maxRotationAngle)
                _minRotationAngle = _maxRotationAngle - 1;
        }

        private void Reset()
        {
            
        }
    }
}
