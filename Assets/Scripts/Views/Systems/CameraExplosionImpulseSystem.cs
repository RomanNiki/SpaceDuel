using Cinemachine;
using Leopotam.Ecs;
using Model.Components.Events;
using Model.VisualEffects.Components.Events;
using UnityEngine;
using Zenject;

namespace Views.Systems
{
    public sealed class CameraExplosionImpulseSystem : IEcsRunSystem
    {
        [Inject] private CinemachineImpulseSource _impulseSource;
        private readonly EcsFilter<ExplosionEvent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                _impulseSource.GenerateImpulseAt(_filter.Get1(i).Position, GetVelocity());
            }
        }

        private static Vector3 GetVelocity()
        {
            var x = Random.Range(-15f, 15f);
            var y = Random.Range(-15f, 15f);
            return new Vector3(x, y, 0f);
        }
    }
}