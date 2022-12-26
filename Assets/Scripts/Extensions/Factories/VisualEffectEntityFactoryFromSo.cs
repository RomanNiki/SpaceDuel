using System;
using Leopotam.Ecs;
using Model.Components.Requests;
using Model.Components.Tags;
using Model.Components.Tags.Effects;
using Model.Timers;
using UnityEngine;

namespace Extensions.Factories
{
    [CreateAssetMenu(fileName = "VisualEffect", menuName = "SpaceDuel/VisualEffects", order = 10)]
    [Serializable]
    public sealed class VisualEffectEntityFactoryFromSo : EntityFactoryFromSo
    {
        [SerializeField] private float _lifeTime;

        public override EcsEntity CreateEntity(EcsWorld world)
        {
            var visualEffect = world.NewEntity();
            visualEffect.Get<VisualEffectTag>();
            visualEffect.Get<GravityResist>();
            visualEffect.Get<ViewCreateRequest>();
            visualEffect.Get<Timer<LifeTime>>().TimeLeftSec = _lifeTime;
            return visualEffect;
        }
    }
}