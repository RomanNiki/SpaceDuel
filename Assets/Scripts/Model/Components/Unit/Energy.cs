using Model.Components.Extensions.Interfaces;

namespace Model.Components.Unit
{
    public struct Energy : ICharacteristic
    {
        public float Initial { get; set; }
        public float Current { get; set; }
    }
}