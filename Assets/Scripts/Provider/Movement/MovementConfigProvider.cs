using System.Threading.Tasks;
using UnityEngine;
using PlayerConfigs;

namespace Providers
{
    public class MovementConfigProvider : ConfigLoaderBase
    {
        private MovementConfig _config;

        public async Task LoadAsync()
        {
            string key = "MovementConfig";

            _config = await LoadBaseAsync<MovementConfig>(key);
        }

        public void Unload()
        {
            UnloadBase();
            _config = null;
        }

        public LayerMask GroundLayer => _config.GroundLayer;

        public LayerMask RoofLayer => _config.RoofLayer;

        public float CheckerRadius => _config.CheckerRadius;

        public float CheckerDistance => _config.CheckerDistance;

        public float GravityValue => _config.GravityValue;

        public float GravityFactor => _config.GravityFactor;

        public float JumpTimeout => _config.JumpTimeout;

        public float TerminalVelocity => _config.TerminalVelocity;
    }
}
