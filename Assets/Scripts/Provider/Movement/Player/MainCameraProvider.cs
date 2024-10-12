using System.Threading.Tasks;
using UnityEngine;

namespace Providers
{
    public class MainCameraProvider : PrefabLoaderBase
    {
        public Camera Prefab { get; private set; }

        public async Task LoadAsync()
        {
            string key = "MainCamera";

            Prefab = await LoadBaseAsync<Camera>(key);
        }

        public void Unload()
        {
            UnloadBase();
            Prefab = null;
        }
    }
}
