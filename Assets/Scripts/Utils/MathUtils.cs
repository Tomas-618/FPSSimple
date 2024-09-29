using UnityEngine;

namespace Utils
{
    public static class MathUtils
    {
        private const float MaxAngle = 360f;

        public static float ClampAngle(float angle, float min, float max)
        {
            if (angle < -MaxAngle)
                angle += MaxAngle;

            if (angle > MaxAngle)
                angle -= MaxAngle;

            return Mathf.Clamp(angle, min, max);
        }
    }
}
