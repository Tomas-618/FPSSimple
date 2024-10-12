using System;
using UnityEngine;

namespace Providers
{
    public class CameraTargetProvider
    {
        public Transform Target { get; private set; }

        public void Set(Transform target) =>
            Target = target != null ? target : throw new ArgumentNullException(nameof(target));
    }
}
