using System;
using Scellecs.Morpeh;

namespace Core.Movement.Components
{
    [Serializable]
    public struct Friction : IComponent
    {
        public float Value;
    }
}