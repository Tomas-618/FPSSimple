using UnityEngine;

namespace Calculators
{
    public class RigidbodyPushingCalculator
    {
        private const float MinMoveDirectionY = -0.3f;

        public void Push(ControllerColliderHit hit, float strength)
        {
            Rigidbody targetRigidbody = hit.rigidbody;

            if (targetRigidbody == null || targetRigidbody.isKinematic)
                return;

            if (hit.moveDirection.y < MinMoveDirectionY)
                return;

            Vector3 pushDirection = new Vector3(hit.moveDirection.x, 0f, hit.moveDirection.z);

            targetRigidbody.AddForce(pushDirection * strength, ForceMode.Impulse);
        }
    }
}
