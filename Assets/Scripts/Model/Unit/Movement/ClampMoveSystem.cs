using Leopotam.Ecs;
using Model.Extensions.Interfaces;
using Model.Unit.Movement.Components;
using Model.VisualEffects.Components.Tags;
using Model.Weapons.Components.Tags;

namespace Model.Unit.Movement
{
    public sealed class ClampMoveSystem : IEcsRunSystem
    {
        private readonly IMoveClamper _moveClamper;
        private readonly EcsFilter<Position>.Exclude<VisualEffectTag, MineTag> _positionFilter = null;

        public ClampMoveSystem(IMoveClamper moveClamper)
        {
            _moveClamper = moveClamper;
        }
        
        public void Run()
        {
            foreach (var i in _positionFilter)
            {
                ref var position = ref _positionFilter.Get1(i);
                position.Value = _moveClamper.ClampPosition(position.Value);
            }
        }
    }
}