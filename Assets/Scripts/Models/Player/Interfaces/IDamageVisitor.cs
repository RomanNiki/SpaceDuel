﻿using Models.Player.Weapon.Bullets;

namespace Models.Player.Interfaces
{
    public interface IDamageVisitor
    {
        float Health { get; }
        void Visit(DamagerModel damager);
        void Visit(IDamageVisitor playerModel);
        void Visit(Sun sun);
    }
}