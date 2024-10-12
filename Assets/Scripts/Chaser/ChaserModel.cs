using System;
using UnityEngine;

public class ChaserModel
{
    public ChaserModel(float speed)
    {
        if (speed < 0f)
            throw new ArgumentOutOfRangeException(speed.ToString());

        Speed = speed;
        Target = null;
        VerticalVelocity = 0f;
    }

    public Transform Target { get; private set; }

    public float Speed { get; private set; }

    public float VerticalVelocity { get; private set; }

    public void SetTarget(Transform target) =>
        Target = target != null ? target : throw new ArgumentNullException(nameof(target));

    public void SetVerticalVelocity(float verticalVelocity) =>
        VerticalVelocity = verticalVelocity;
}
