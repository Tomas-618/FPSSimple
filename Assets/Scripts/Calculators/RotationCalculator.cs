using UnityEngine;
using Utils;

namespace Calculators
{
    public class RotationCalculator
    {
        private const float Threshold = 0.001f;

        public void SetRotation(ref float pitch, out float yaw,
            float sensitivity, float minAngle, float maxAngle, Vector2 rotation)
        {
            yaw = 0f;

            if (rotation.sqrMagnitude < Threshold)
                return;

            pitch += rotation.y * sensitivity;
            yaw = rotation.x * sensitivity;

            pitch = MathUtils.ClampAngle(pitch, minAngle, maxAngle);
        }
    }
}
