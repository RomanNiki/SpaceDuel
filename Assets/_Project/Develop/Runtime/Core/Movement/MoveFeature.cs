using _Project.Develop.Runtime.Core.Movement.Components;
using _Project.Develop.Runtime.Core.Movement.Components.Events;
using _Project.Develop.Runtime.Core.Movement.Systems;
using Scellecs.Morpeh.Addons.Feature;

namespace _Project.Develop.Runtime.Core.Movement
{
    public class MoveFeature : FixedUpdateFeature
    {
        private readonly IMoveLoopService _loopService;

        public MoveFeature(IMoveLoopService loopService)
        {
            _loopService = loopService;
        }
        
        protected override void Initialize()
        {
            RegisterEvent<StartAccelerationEvent>();
            RegisterEvent<StopAccelerationEvent>();
            RegisterEvent<StartRotationEvent>();
            RegisterEvent<StopRotationEvent>();
            AddSystem(new RotateSystem());
            AddSystem(new ForceSystem());
            AddSystem(new MoveEventSystem());
            AddSystem(new GravitySystem());
            AddSystem(new AccelerationSystem());
            AddSystem(new VelocitySystem());
            AddSystem(new FrictionSystem());
            AddSystem(new ProjectileLookAtVelocitySystem());
            AddSystem(new MoveClampSystem(_loopService));     
            AddSystem(new FollowSystem());
            AddSystem(new ExecuteMoveSystem());
            RegisterRequest<ForceRequest>();
        }
    }
}