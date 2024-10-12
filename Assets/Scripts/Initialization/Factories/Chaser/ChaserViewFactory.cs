using Providers;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Factories
{
    public class ChaserViewFactory
    {
        private readonly ChaserViewPrefabProvider _provider;
        private readonly Vector3 _spawnPosition;

        public ChaserViewFactory(ChaserViewPrefabProvider provider)
        {
            _provider = provider ?? throw new System.ArgumentNullException(nameof(provider));
            _spawnPosition = new Vector3(0f, 1f, -10f);
        }

        public ChaserView Create()
        {
            ChaserView view = Object.Instantiate(_provider.Prefab, _spawnPosition, Quaternion.identity);

            view.Init();

            return view;
        }
    }
}
