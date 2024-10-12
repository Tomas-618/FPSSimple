using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ChaserView : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [field: SerializeField] public Transform LegsTransform { get; private set; }

    public Transform Transform { get; private set; }

    public void Init()
    {
        Transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void MoveHorizontal(Vector3 horizontalVelocity)
    {
        Vector3 velocity = _rigidbody.velocity;

        velocity.x = horizontalVelocity.x;
        velocity.z = horizontalVelocity.z;

        _rigidbody.velocity = velocity;
    }

    public void MoveVertical(float verticalVelocity)
    {
        Vector3 velocity = _rigidbody.velocity;

        velocity.y = verticalVelocity;

        _rigidbody.velocity = velocity;
    }

    public void LookAt(Vector3 direction) =>
        Transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
}
