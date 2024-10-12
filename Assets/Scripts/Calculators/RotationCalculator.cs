using UnityEngine;

namespace Calculators
{
    public class RotationCalculator
    {
        private const float MaxAngle = 360f;
        private const float Threshold = 0.001f;

        public void SetRotation(ref float pitch, out float yaw,
            float sensitivity, float minAngle, float maxAngle, Vector2 rotation)
        {
            yaw = 0f;

            if (rotation.sqrMagnitude < Threshold)
                return;

            pitch += rotation.y * sensitivity;
            yaw = rotation.x * sensitivity;

            pitch = ClampAngle(pitch, minAngle, maxAngle);
        }

        private float ClampAngle(float angle, float min, float max)
        {
            if (angle < -MaxAngle)
                angle += MaxAngle;

            if (angle > MaxAngle)
                angle -= MaxAngle;

            return Mathf.Clamp(angle, min, max);
        }
    }
}
