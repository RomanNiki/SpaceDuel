using Leopotam.Ecs;
using Model.Components.Events;
using Model.Components.Extensions;
using Model.Components.Requests;
using Model.Components.Unit;
using UnityEngine;

namespace Model.Systems.Unit.Collisions
{
    public sealed class DamageContainerTriggerSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ContainerComponents<TriggerEnterEvent>, DamageContainer> _filter =
            null;

        public void Run()
        {
            foreach (var i in _filter)
            {
              ref var bullet = ref _filter.GetEntity(i);
                ref var bulletHealthCurrent = ref bullet.Get<Health>();

                var collisions = _filter.Get1(i).List;

                foreach (var collision in collisions)
                {
                    var calculateBulletHealthCurrent = GetCalculateBulletHealthCurrent(bullet, bulletHealthCurrent.Current);
                    if (calculateBulletHealthCurrent == 0) break;
                    var otherEntity = collision.Other;
                    ProcessBulletCollision(bullet, otherEntity);
                }
            }
        }

        private static float GetCalculateBulletHealthCurrent(in EcsEntity bullet, in float healthCurrent)
        {
            var damage = bullet.Has<DamageRequest>() ? bullet.Get<DamageRequest>().Damage : 0;
            return Mathf.Clamp(healthCurrent - damage, 0, int.MaxValue);
        }

        private static void ProcessBulletCollision(in EcsEntity bullet, in EcsEntity otherEntity)
        {
            if (!otherEntity.IsAlive()) return;
            if (otherEntity.Has<Health>() == false) return;

            var damage = bullet.Get<DamageContainer>().DamageRequest.Damage;

            otherEntity.Get<DamageRequest>().Damage += damage;

            bullet.Get<DamageRequest>().Damage = bullet.Get<Health>().Current;
        }
    }
}