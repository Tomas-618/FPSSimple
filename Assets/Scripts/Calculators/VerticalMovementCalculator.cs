using UnityEngine;

namespace Calculators
{
    public class VerticalMovementCalculator
    {
        public void Jump(ref float verticalVelocity, float gravityFactor, float gravityValue, float jumpHeight) =>
            verticalVelocity = Mathf.Sqrt(jumpHeight * gravityFactor * gravityValue);

        public void SetVelocityOnFalling(ref float verticalVelocity, float terminalVelocity, float gravityValue)
        {
            if (verticalVelocity < terminalVelocity)
                verticalVelocity -= gravityValue * Time.deltaTime;
        }

        public void SetVelocityOnCollising(ref float verticalVelocity) =>
            verticalVelocity = -1;
    }
}
