using Core.Movement.Components;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "GameEntities/GameEntity", fileName = nameof(GameEntitySettings))]
    public class GameEntitySettings : ScriptableObject
    {
        [field: SerializeField] public Friction Friction { get; private set; }
        [field: SerializeField] public Mass Mass { get; private set; }
        [field: SerializeField] public float MaxHealth { get; private set; }
    }
}