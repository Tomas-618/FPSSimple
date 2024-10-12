using Providers;
using System;

namespace Factories
{
    public class ChaserModelFactory
    {
        private readonly ChaserModelConfigProvider _modelConfig;

        public ChaserModelFactory(ChaserModelConfigProvider modelConfig) =>
            _modelConfig = modelConfig ?? throw new ArgumentNullException(nameof(modelConfig));

        public ChaserModel Create() =>
            new ChaserModel(_modelConfig.Config.WalkingSpeed);
    }
}
