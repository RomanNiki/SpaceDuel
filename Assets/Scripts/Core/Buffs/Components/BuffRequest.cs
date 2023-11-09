using System;
using Scellecs.Morpeh;

namespace Core.Buffs.Components
{
    [Serializable]
    public struct BuffRequest : IComponent
    {
        public Entity Buff;
        public Entity Player;

        public BuffRequest(Entity buff, Entity player)
        {
            Buff = buff;
            Player = player;
        }
    }
}