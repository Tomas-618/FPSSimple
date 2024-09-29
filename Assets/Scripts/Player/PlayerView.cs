using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerView : MonoBehaviour
{
    [SerializeField, Min(0)] private float _radius;

    [SerializeField, Min(0)] private float _distance;

    private CharacterController _characterController;

    [field: SerializeField] public LayerMask GroundLayer { get; private set; }

    [field: SerializeField] public LayerMask RoofLayer { get; private set; }

    [field: SerializeField] public Transform CameraTarget { get; private set; }

    [field: SerializeField] public Transform HeadTransform { get; private set; }

    [field: SerializeField] public Transform LegsTransform { get; private set; }

    public Transform Transform { get; private set; }

    public float Radius => _radius;

    public float Distance => _distance;

    public void Init()
    {
        Transform = transform;
        _characterController = GetComponent<CharacterController>();
    }

    public void Move(Vector3 velocity) =>
        _characterController.Move(velocity);

    public void Rotate(float pitch, float yaw)
    {
        CameraTarget.localRotation = Quaternion.Euler(Vector3.right * pitch);

        Transform.Rotate(Vector3.up * yaw);
    }
}
