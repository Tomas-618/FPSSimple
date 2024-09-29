using UnityEngine;
using PlayerConfigs;
using System;

namespace Calculators
{
    public class VerticalMovementCalculator
    {
        private readonly PlayerConfig _config;

        private float _jumpTimeoutDelta;

        public VerticalMovementCalculator(PlayerConfig config)
        {
            _config = config != null ? config : throw new ArgumentNullException(nameof(config));
            _jumpTimeoutDelta = _config.JumpTimeout;
        }

        public bool TryJump(ref float verticalVelocity, float jumpHeight)
        {
            if (_jumpTimeoutDelta > 0f)
                return false;

            verticalVelocity = Mathf.Sqrt(jumpHeight * _config.GravityFactor * _config.GravityValue);

            return true;
        }

        public void SetVelocityOnGround(ref float verticalVelocity)
        {
            SetVelocityOnCollising(ref verticalVelocity);

            if (_jumpTimeoutDelta >= 0f)
                _jumpTimeoutDelta -= Time.deltaTime;
        }

        public void SetVelocityOnFalling(ref float verticalVelocity)
        {
            _jumpTimeoutDelta = _config.JumpTimeout;

            if (verticalVelocity < _config.TerminalVelocity)
                verticalVelocity -= _config.GravityValue * Time.deltaTime;
        }

        public void SetVelocityOnCollising(ref float verticalVelocity) =>
            verticalVelocity = -1;
    }
}
