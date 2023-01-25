using Model.Extensions.Interfaces;

namespace Model.Unit.EnergySystems.Components
{
    public struct Energy : ICharacteristic
    {
        public float Initial { get; set; }
        public float Current { get; set; }
    }
}