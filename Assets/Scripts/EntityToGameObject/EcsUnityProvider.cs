using Leopotam.Ecs;
using UnityEngine;

namespace EntityToGameObject
{
    public sealed class EcsUnityProvider : MonoBehaviour
    {
        public ref EcsEntity Entity
        {
            get
            {
                if(_entity.IsNull()) Debug.LogWarning("Entity is not assigned!");
                return ref _entity;
            }
        }

        private EcsEntity _entity;

        public void SetEntity(in EcsEntity entity) => _entity = entity;
    }
}