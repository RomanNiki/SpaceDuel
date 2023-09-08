using Core.Extensions;
using Core.Movement.Gravity.Systems;
using Core.Movement.Systems;
using Cysharp.Threading.Tasks;

namespace Core.Movement
{
    public class MoveFeature : BaseMorpehFeature
    {
        private readonly IMoveLoopService _loopService;

        public MoveFeature(IMoveLoopService loopService)
        {
            _loopService = loopService;
        }

        protected override async UniTask InitializeSystems()
        {
            AddSystem(new NoEnergyGravityResistSystem());
            AddSystem(new RotateSystem());
            AddSystem(new ForceSystem());
            AddSystem(new GravitySystem());
            AddSystem(new AccelerationSystem());
            AddSystem(new VelocitySystem());
            AddSystem(new FrictionSystem());
            AddSystem(new ProjectileLookAtVelocitySystem());
            AddSystem(new MoveClampSystem(_loopService));
            AddSystem(new ExecuteMoveSystem());
        }
    }
}