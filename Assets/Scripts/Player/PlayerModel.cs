using System;

public class PlayerModel
{
    public PlayerModel(float walkingSpeed, float runningSpeed,
        float speedChangeRate, float sensitivity, float minRotationAngle,
        float maxRotationAngle, float strength, float jumpHeight)
    {
        if (walkingSpeed < 0f)
            throw new ArgumentOutOfRangeException(walkingSpeed.ToString());

        if (runningSpeed < 0f)
            throw new ArgumentOutOfRangeException(runningSpeed.ToString());

        if (speedChangeRate < 0f)
            throw new ArgumentOutOfRangeException(speedChangeRate.ToString());

        if (sensitivity < 0f)
            throw new ArgumentOutOfRangeException(sensitivity.ToString());

        if (strength < 0f)
            throw new ArgumentOutOfRangeException(strength.ToString());

        if (jumpHeight < 0f)
            throw new ArgumentOutOfRangeException(jumpHeight.ToString());

        if (walkingSpeed >= runningSpeed)
            throw new InvalidOperationException(nameof(walkingSpeed) +
                $"({walkingSpeed}) should be less than {nameof(runningSpeed)}({runningSpeed})");

        if (minRotationAngle >= maxRotationAngle)
            throw new InvalidOperationException(nameof(minRotationAngle) +
                $"({minRotationAngle}) should be less than" +
                $"{nameof(maxRotationAngle)}({maxRotationAngle})");

        WalkingSpeed = walkingSpeed;
        RunningSpeed = runningSpeed;
        SpeedChangeRate = speedChangeRate;
        Sensitivity = sensitivity;
        MinRotationAngle = minRotationAngle;
        MaxRotationAngle = maxRotationAngle;
        Strength = strength;
        JumpHeight = jumpHeight;

        Speed = walkingSpeed;
        Pitch = 0f;
        VerticalVelocity = 0f;
    }

    public float WalkingSpeed { get; private set; }

    public float RunningSpeed { get; private set; }

    public float SpeedChangeRate { get; private set; }

    public float Speed { get; private set; }

    public float Sensitivity { get; private set; }

    public float MinRotationAngle { get; private set; }

    public float MaxRotationAngle { get; private set; }

    public float Pitch { get; private set; }

    public float Strength { get; private set; }

    public float VerticalVelocity { get; private set; }

    public float JumpHeight { get; private set; }

    public void SetSpeed(float speed)
    {
        if (speed < WalkingSpeed || speed > RunningSpeed)
            throw new ArgumentOutOfRangeException(speed.ToString());

        Speed = speed;
    }

    public void SetPitch(float pitch)
    {
        if (pitch < MinRotationAngle || pitch > MaxRotationAngle)
            throw new ArgumentOutOfRangeException(pitch.ToString());

        Pitch = pitch;
    }

    public void SetVerticalVelocity(float verticalVelocity) =>
        VerticalVelocity = verticalVelocity;
}
