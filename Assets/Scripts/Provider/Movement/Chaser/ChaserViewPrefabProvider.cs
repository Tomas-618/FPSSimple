using System.Threading.Tasks;

namespace Providers
{
    public class ChaserViewPrefabProvider : PrefabLoaderBase
    {
        public ChaserView Prefab { get; private set; }

        public async Task LoadAsync()
        {
            string key = "ChaserView";

            Prefab = await LoadBaseAsync<ChaserView>(key);
        }

        public void Unload()
        {
            UnloadBase();
            Prefab = null;
        }
    }
}
