using System;
using Calculators;

namespace Factories
{
    public class PlayerPresenterFactory
    {
        private readonly RigidbodyPushingCalculator _rigidbodyPushingCalculator;

        public PlayerPresenterFactory(RigidbodyPushingCalculator rigidbodyPushingCalculator) =>
            _rigidbodyPushingCalculator = rigidbodyPushingCalculator ??
            throw new ArgumentNullException(nameof(rigidbodyPushingCalculator));

        public PlayerPresenter Create(PlayerModel model) =>
            new PlayerPresenter(model, _rigidbodyPushingCalculator);
    }
}
