using System;
using Model.Extensions.Interfaces;

namespace Model.Unit.Damage.Components
{
    [Serializable]
    public struct Health : ICharacteristic
    {
        public float Current { get; set; }
        public float Initial { get; set; }
    }
}