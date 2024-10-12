using System.Threading.Tasks;
using Configs;

namespace Providers
{
    public class BotsConfigProvider : ConfigLoaderBase
    {
        private BotsConfig _config;

        public float SquareDistanceToInteract => _config.SquareDistanceToInteract;

        public async Task LoadAsync()
        {
            string key = "BotsConfig";

            _config = await LoadBaseAsync<BotsConfig>(key);
        }

        public void Unload()
        {
            UnloadBase();
            _config = null;
        }
    }
}
