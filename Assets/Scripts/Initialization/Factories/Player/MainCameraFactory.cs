using System;
using UnityEngine;
using Providers;
using Object = UnityEngine.Object;

namespace Factories
{
    public class MainCameraFactory
    {
        private readonly MainCameraProvider _provider;

        public MainCameraFactory(MainCameraProvider provider) =>
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));

        public Camera Create()
        {
            Camera camera = Object.Instantiate(_provider.Prefab);

            return camera;
        }
    }
}
