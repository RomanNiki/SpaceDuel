using Core.Movement.Components;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "GameEntities/ShipSettings", fileName = nameof(ShipSettings))]
    public class ShipSettings : GameEntitySettings
    {
        [field: SerializeField] public float MaxEnergy { get; private set; }
        [field: SerializeField] public Speed Speed { get; private set; }
        [field: SerializeField] public RotationSpeed RotationSpeed { get; private set; }
        [field: SerializeField] public float AccelerateDischargeAmount { get; private set; }
        [field: SerializeField] public float RotateDischargeAmount { get; private set; }
    }
}