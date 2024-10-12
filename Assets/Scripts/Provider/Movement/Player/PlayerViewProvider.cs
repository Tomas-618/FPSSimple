using System;

namespace Providers
{
    public class PlayerViewProvider
    {
        public PlayerView View { get; private set; }

        public void Set(PlayerView view) =>
            View = view != null ? view : throw new ArgumentNullException(nameof(view));
    }
}
