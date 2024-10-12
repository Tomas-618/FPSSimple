using System.Threading.Tasks;
using Cinemachine;

namespace Providers
{
    public class PlayerFollowCameraProvider : PrefabLoaderBase
    {
        public CinemachineVirtualCamera Prefab { get; private set; }

        public async Task LoadAsync()
        {
            string key = "PlayerFollowCamera";

            Prefab = await LoadBaseAsync<CinemachineVirtualCamera>(key);
        }

        public void Unload()
        {
            UnloadBase();
            Prefab = null;
        }
    }
}
