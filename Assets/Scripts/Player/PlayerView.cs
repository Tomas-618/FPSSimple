using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerView : MonoBehaviour
{
    private PlayerPresenter _presenter;
    private CharacterController _characterController;

    [field: SerializeField] public Transform CameraTarget { get; private set; }

    [field: SerializeField] public Transform HeadTransform { get; private set; }

    [field: SerializeField] public Transform LegsTransform { get; private set; }

    public Transform Transform { get; private set; }

    private void OnControllerColliderHit(ControllerColliderHit hit) =>
        _presenter.OnColliderHit(hit);

    public void Init(PlayerPresenter presenter)
    {
        Transform = transform;
        _presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
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
