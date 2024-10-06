using System;
using UnityEngine;
using Providers;
using Object = UnityEngine.Object;

namespace Factories
{
    public class PlayerViewFactory
    {
        private readonly PlayerViewProvider _provider;
        private readonly Vector3 _spawnPosition;

        public PlayerViewFactory(PlayerViewProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));

            _spawnPosition = Vector3.zero;
        }

        public PlayerView Create(PlayerPresenter presenter)
        {
            PlayerView prefab = Object.Instantiate(_provider.View, _spawnPosition, Quaternion.identity);

            prefab.Init(presenter);

            return prefab;
        }
    }
}
