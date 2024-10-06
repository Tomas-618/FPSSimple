using System;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace Providers
{
    public class ObjectLoaderBase
    {
        //private ScriptableObject _cashed;

        //protected async Task<T> LoadBaseAsync<T>(string configID) where T : ScriptableObject
        //{
        //    var handle = Addressables.LoadAssetAsync<T>(configID);

        //    _cashed = await handle.Task;

        //    if (_cashed is not T)
        //        throw new ArgumentException(configID);

        //    return _cashed as T;
        //}

        private Object _cashed;

        protected async Task<T> LoadBaseAsync<T>(string configID) where T : Object
        {
            var handle = Addressables.LoadAssetAsync<T>(configID);

            _cashed = await handle.Task;

            if (_cashed is not T)
                throw new ArgumentException(configID);

            return _cashed as T;
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
