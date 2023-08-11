using System;
using Core.Enums;
using Scellecs.Morpeh;

namespace Core.Player.Components
{
    [Serializable]
    public struct Team : IComponent
    {
        public TeamEnum Value;
    }
}