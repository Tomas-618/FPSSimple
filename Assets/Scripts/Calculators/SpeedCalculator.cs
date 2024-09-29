using UnityEngine;

namespace Calculators
{
    public class SpeedCalculator
    {
        public void SetSpeed(ref float speed, float walkingSpeed,
            float runningSpeed, float speedChangeRate, bool isRunning)
        {
            float desiredSpeed = isRunning ? runningSpeed : walkingSpeed;

            if (Mathf.Approximately(speed, desiredSpeed))
                return;

            speed = Mathf.Lerp(speed, desiredSpeed, Time.deltaTime * speedChangeRate);
            speed = Mathf.Clamp(speed, walkingSpeed, runningSpeed);
        }
    }
}
