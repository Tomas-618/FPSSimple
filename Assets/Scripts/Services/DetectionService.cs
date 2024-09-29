using UnityEngine;

namespace Services
{
    public class DetectionService
    {
        private readonly RaycastHit[] _hits;

        public DetectionService() =>
            _hits = new RaycastHit[1];

        public bool Check(Vector3 position, float radius, LayerMask layerMask) =>
            Physics.CheckSphere(position, radius, layerMask, QueryTriggerInteraction.Ignore);

        public Vector3 GetDirectionAlongSurface(Vector3 position, Vector3 direction, float distance, LayerMask layerMask)
        {
            const float Epsilon = 0.01f;

            if (direction == Vector3.zero)
                return direction;

            Ray ray = new Ray(position, Vector3.down);

            QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.Ignore;

            int count = Physics.RaycastNonAlloc(ray, _hits, distance, layerMask, queryTriggerInteraction);
            
            if (count <= 0)
                return direction;

            Vector3 normal = _hits[0].normal;

            float normalAngle = Vector3.Angle(normal, Vector3.up);

            if (Mathf.Approximately(normalAngle, 0))
                return direction;

            Vector3 startPosition = _hits[0].point;

            ray.origin = position + (direction * Epsilon);
            count = Physics.RaycastNonAlloc(ray, _hits, distance, layerMask, queryTriggerInteraction);

            if (count <= 0)
                return direction;

            Vector3 endPosition = _hits[0].point;

            return (endPosition - startPosition).normalized;
        }
    }
}
