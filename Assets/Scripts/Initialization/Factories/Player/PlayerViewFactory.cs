using System;
using UnityEngine;
using Providers;
using Object = UnityEngine.Object;

namespace Factories
{
    public class PlayerViewFactory
    {
        private readonly PlayerViewPrefabProvider _prefabProvider;
        private readonly PlayerViewProvider _provider;
        private readonly CameraTargetProvider _cameraTargetProvider;
        private readonly Vector3 _spawnPosition;

        public PlayerViewFactory(PlayerViewPrefabProvider prefabProvider, PlayerViewProvider provider,
            CameraTargetProvider cameraTargetProvider)
        {
            _prefabProvider = prefabProvider ?? throw new ArgumentNullException(nameof(prefabProvider));
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
            _cameraTargetProvider = cameraTargetProvider ?? throw new ArgumentNullException(nameof(cameraTargetProvider));

            _spawnPosition = Vector3.zero;
        }

        public PlayerView Create(PlayerPresenter presenter)
        {
            PlayerView view = Object.Instantiate(_prefabProvider.Prefab, _spawnPosition, Quaternion.identity);

            view.Init(presenter);

            _provider.Set(view);
            _cameraTargetProvider.Set(view.CameraTarget);

            return view;
        }
    }
}
