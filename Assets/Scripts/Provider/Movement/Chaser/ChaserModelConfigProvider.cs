using System.Threading.Tasks;
using Configs;

namespace Providers
{
    public class ChaserModelConfigProvider : ConfigLoaderBase
    {
        public MoverModelConfig Config { get; private set; }

        public async Task LoadAsync()
        {
            string key = "ChaserModelConfig";

            Config = await LoadBaseAsync<MoverModelConfig>(key);
        }

        public void Unload()
        {
            UnloadBase();
            Config = null;
        }
    }
}
