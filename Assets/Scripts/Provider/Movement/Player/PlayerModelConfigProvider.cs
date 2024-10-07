using System.Threading.Tasks;
using PlayerConfigs;

namespace Providers
{
    public class PlayerModelConfigProvider : ConfigLoaderBase
    {
        public PlayerModelConfig Config { get; private set; }

        public async Task LoadAsync()
        {
            string key = "PlayerModelConfig";

            Config = await LoadBaseAsync<PlayerModelConfig>(key);
        }

        public void Unload()
        {
            UnloadBase();
            Config = null;
        }
    }
}
