using Cinemachine;
using Providers;
using Object = UnityEngine.Object;

namespace Factories
{
    public class PlayerFollowCameraFactory
    {
        private readonly PlayerFollowCameraProvider _provider;
        private readonly CameraTargetProvider _targetProvider;

        public PlayerFollowCameraFactory(PlayerFollowCameraProvider provider, CameraTargetProvider targetProvider)
        {
            _provider = provider ?? throw new System.ArgumentNullException(nameof(provider));
            _targetProvider = targetProvider ?? throw new System.ArgumentNullException(nameof(targetProvider));
        }

        public CinemachineVirtualCamera Create()
        {
            CinemachineVirtualCamera virtualCamera = Object.Instantiate(_provider.Prefab);

            virtualCamera.Follow = _targetProvider.Target;

            return virtualCamera;
        }
    }
}
