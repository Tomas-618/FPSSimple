using System;
using UnityEngine;

namespace Services
{
    public class MonoBehavioursMethodsService
    {
        public event Action Updated = delegate { };

        public event Action LateUpdated = delegate { };

        public event Action<ControllerColliderHit> ColliderHit = delegate { };

        public void OnUpdate() =>
            Updated?.Invoke();

        public void OnLateUpdate() =>
            LateUpdated?.Invoke();

        public void OnColliderHit(ControllerColliderHit hit) =>
            ColliderHit?.Invoke(hit);
    }
}
