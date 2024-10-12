using UnityEngine;

namespace Services
{
    public class TargetOperationsService
    {
        public bool IsNearTo(Vector3 position, Vector3 target, float minSquareDistance) =>
            (target - position).sqrMagnitude <= minSquareDistance;

        public Vector3 GetDirectionOnHorizontalPlane(Vector3 startPosition, Vector3 target)
        {
            Vector3 direction = target - startPosition;

            direction.y = 0f;

            return direction.normalized;
        }
    }
}
