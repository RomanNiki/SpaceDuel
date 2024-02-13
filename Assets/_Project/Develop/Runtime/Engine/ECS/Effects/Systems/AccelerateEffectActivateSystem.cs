using _Project.Develop.Runtime.Core.Input.Components;
using _Project.Develop.Runtime.Engine.ECS.Effects.Components;
using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Engine.ECS.Effects.Systems
{
    public sealed class AccelerateEffectActivateSystem : ISystem
    {
        private Filter _filter;
        private Stash<InputMoveData> _inputDataPool;
        private Stash<AcceleratePlayerEffect> _acceleratePlayerEffectPool;
        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter.With<AcceleratePlayerEffect>().With<InputMoveData>().Build();
            _acceleratePlayerEffectPool = World.GetStash<AcceleratePlayerEffect>();
            _inputDataPool = World.GetStash<InputMoveData>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var inputData = ref _inputDataPool.Get(entity);
                ref var acceleratePlayerEffect = ref _acceleratePlayerEffectPool.Get(entity);
                if (inputData.Accelerate)
                {
                    acceleratePlayerEffect.EffectController.Play();
                }
                else
                {
                    acceleratePlayerEffect.EffectController.Stop();
                }
            }
        }
        
        public void Dispose()
        {
        }
    }
}