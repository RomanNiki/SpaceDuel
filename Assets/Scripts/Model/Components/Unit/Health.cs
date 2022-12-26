using System;
using Model.Components.Extensions.Interfaces;

namespace Model.Components.Unit
{
    [Serializable]
    public struct Health : ICharacteristic
    {
        public float Current { get; set; }
        public float Initial { get; set; }
    }
}