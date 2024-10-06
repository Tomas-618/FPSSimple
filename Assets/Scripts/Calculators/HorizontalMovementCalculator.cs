using System;
using UnityEngine;
using Services;

namespace Calculators
{
    public class HorizontalMovementCalculator
    {
        private readonly DetectionService _detectionService;

        public HorizontalMovementCalculator(DetectionService detectionService) =>
            _detectionService = detectionService ?? throw new ArgumentNullException(nameof(detectionService));

        public Vector3 GetVelocity(Transform transform, Vector3 legsPosition, Vector3 inputDirection,
            float speed, float checkerDistance, LayerMask layerMasks)
        {
            Vector3 direction = transform.TransformDirection(inputDirection);

            Debug.DrawRay(transform.position, direction, Color.blue);

            return Time.deltaTime * speed *
                _detectionService.GetDirectionAlongSurface(legsPosition, direction, checkerDistance, layerMasks);
        }
    }
}
