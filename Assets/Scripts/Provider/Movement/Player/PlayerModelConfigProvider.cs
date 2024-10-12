using System.Threading.Tasks;
using Configs;

namespace Providers
{
    public class PlayerModelConfigProvider : ConfigLoaderBase
    {
        public MoverModelConfig Config { get; private set; }

        public async Task LoadAsync()
        {
            string key = "PlayerModelConfig";

            Config = await LoadBaseAsync<MoverModelConfig>(key);
        }

        public void Unload()
        {
            UnloadBase();
            Config = null;
        }
    }
}
