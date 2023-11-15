namespace _Project.Develop.Runtime.Core.Extensions
{
    public interface IStat
    {
        public float MaxValue { get; }
        public float Value { get; }
        public float BaseValue { get; }
        public float MinValue { get; }
    }
}