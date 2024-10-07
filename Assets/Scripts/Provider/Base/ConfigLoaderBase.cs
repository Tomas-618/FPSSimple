using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Providers
{
    public class ConfigLoaderBase
    {
        private ScriptableObject _cashed;

        protected async Task<T> LoadBaseAsync<T>(string configID) where T : ScriptableObject
        {
            var handle = Addressables.LoadAssetAsync<T>(configID);

            _cashed = await handle.Task;

            T cashed = _cashed as T;

            if (cashed == null)
                throw new ArgumentException(configID);

            return cashed;
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
