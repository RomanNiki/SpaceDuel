﻿using Core.Characteristics.Damage.Components;
using Core.Extensions;
using Core.Movement.Components;
using Scellecs.Morpeh;

namespace Core.Collisions.Strategies
{
    public class DamageTargetStrategy : IEnterTriggerStrategy
    {
        public void OnEnter(World world, Entity sender, Entity target)
        {
            var damages = world.GetStash<Damage>();
            var positions = world.GetStash<Position>();
            var damage = damages.Get(sender);
            var position = positions.Get(sender);
           

            world.SendMessage(new DamageRequest(damage.Value, position.Value, target));
        }
    }
}