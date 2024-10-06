using System;
using UnityEngine;
using Calculators;

public class PlayerPresenter
{
    private readonly PlayerModel _model;
    private readonly RigidbodyPushingCalculator _rigidbodyPushingCalculator;

    public PlayerPresenter(PlayerModel model, RigidbodyPushingCalculator rigidbodyPushingCalculator)
    {
        _model = model ?? throw new ArgumentNullException(nameof(model));
        _rigidbodyPushingCalculator = rigidbodyPushingCalculator ??
            throw new ArgumentNullException(nameof(rigidbodyPushingCalculator));
    }

    public void OnColliderHit(ControllerColliderHit hit) =>
        _rigidbodyPushingCalculator.Push(hit, _model.Strength);
}
