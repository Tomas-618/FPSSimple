using System;

namespace Services
{
    public class UpdateService
    {
        public event Action Updated = delegate { };

        public event Action FixedUpdated = delegate { };

        public event Action LateUpdated = delegate { };

        public void OnUpdate() =>
            Updated?.Invoke();

        public void OnFixedUpdate() =>
            FixedUpdated?.Invoke();

        public void OnLateUpdate() =>
            LateUpdated?.Invoke();
    }
}
