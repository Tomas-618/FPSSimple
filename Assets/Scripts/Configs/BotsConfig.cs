using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "BotsConfig", menuName = "Configs/BotsConfig", order = 53)]
    public class BotsConfig : ScriptableObject
    {
        [SerializeField, Min(0)] private float _squareDistanceToInteract;

        public float SquareDistanceToInteract => _squareDistanceToInteract;
    }
}
