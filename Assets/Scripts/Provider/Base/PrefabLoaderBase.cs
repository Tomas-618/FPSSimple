using System.Threading.Tasks;
using System;
using UnityEngine.AddressableAssets;
using UnityEngine;

namespace Providers
{
    public class PrefabLoaderBase
    {
        private GameObject _cashed;

        protected async Task<T> LoadBaseAsync<T>(string configID)
        {
            _cashed = await Addressables.LoadAssetAsync<GameObject>(configID).Task;

            if (_cashed.TryGetComponent(out T component) == false)
                throw new ArgumentException(nameof(component));

            return component;
        }

        protected void UnloadBase()
        {
            if (_cashed == null)
                return;

            Addressables.Release(_cashed);
            _cashed = null;
        }
    }
}
