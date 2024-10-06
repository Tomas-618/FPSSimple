using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ChaserView : MonoBehaviour
{
    private Transform _transform;
    private Rigidbody _rigidbody;

    public void Init()
    {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 move) =>
        _rigidbody.velocity = move;

    public void Rotate(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - _transform.position;

        direction.y = 0f;

        _transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }
}
