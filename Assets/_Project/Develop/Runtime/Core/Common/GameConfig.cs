namespace _Project.Develop.Runtime.Core.Common
{
    public static class GameConfig
    {
        public const float G = 0.06674f; //6,7 * 10^-11
        public const float MinRotationForDischarge = 0.2f;
        public const float MinChargeAmount = 0.01f;
        public const float MinRotationCoefficient = 0.01f;
        public const float MinDischargeAmount = 0.01f;
        public const float DefaultTimeScale = 1f;
        public const float AcceleratedTimeScale = 3f;
        public const float MaximumPositionDeviation = 0.1f;
        public const float RotationThreshold = 0.1f;
    }
}