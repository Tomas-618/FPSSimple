using PlayerConfigs;
using Services;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private PlayerConfig _playerConfig;
    [SerializeField] private PlayerModelConfig _playerModelConfig;

    private PlayerController _playerController;
    private MonoBehavioursMethodsService _monoBehavioursMethodsService;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        _playerView.Init();

        _monoBehavioursMethodsService = new MonoBehavioursMethodsService();
        _playerController = new PlayerControllerFactory(_playerView, _playerConfig,
            _playerModelConfig, _monoBehavioursMethodsService).Create();
    }

    private void Update() =>
        _monoBehavioursMethodsService.OnUpdate();

    private void LateUpdate() =>
        _monoBehavioursMethodsService.OnLateUpdate();

    private void OnControllerColliderHit(ControllerColliderHit hit) =>
        _monoBehavioursMethodsService.OnColliderHit(hit);
}
