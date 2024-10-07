using System.Threading.Tasks;

namespace Providers
{
    public class PlayerViewProvider : PrefabLoaderBase
    {
        public PlayerView View { get; private set; }

        public async Task LoadAsync()
        {
            string key = "PlayerView";

            View = await LoadBaseAsync<PlayerView>(key);
        }

        public void Unload()
        {
            UnloadBase();
            View = null;
        }
    }
}
