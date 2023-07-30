using Leopotam.Ecs;
using Model.Extensions;
using Model.Unit.Collisions.Components.Events;
using Model.Unit.Damage.Components;
using Model.Unit.Damage.Components.Requests;
using UnityEngine;

namespace Model.Unit.Collisions
{
    public sealed class DamageContainerTriggerSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ContainerComponents<TriggerEnterEvent>, DamageContainer> _damagerFilter =
            null;

        public void Run()
        {
            foreach (var i in _damagerFilter)
            {
                ref var bullet = ref _damagerFilter.GetEntity(i);
                ref var bulletHealthCurrent = ref bullet.Get<Health>();
                
                var collisions = _damagerFilter.Get1(i).List;

                for (var j = 0; j < collisions.Count; j++)
                {
                    var collision = collisions.Dequeue();
                    var calculateBulletHealthCurrent =
                        GetCalculateBulletHealthCurrent(bullet, bulletHealthCurrent.Current);
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