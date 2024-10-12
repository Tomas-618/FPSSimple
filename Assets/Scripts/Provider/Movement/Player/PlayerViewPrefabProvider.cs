using System.Threading.Tasks;

namespace Providers
{
    public class PlayerViewPrefabProvider : PrefabLoaderBase
    {
        public PlayerView Prefab { get; private set; }

        public async Task LoadAsync()
        {
            string key = "PlayerView";

            Prefab = await LoadBaseAsync<PlayerView>(key);
        }

        public void Unload()
        {
            UnloadBase();
            Prefab = null;
        }
    }
}
