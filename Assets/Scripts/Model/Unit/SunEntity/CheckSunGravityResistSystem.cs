using Leopotam.Ecs;
using Model.Unit.EnergySystems.Components;
using Model.Unit.Movement.Components.Tags;

namespace Model.Unit.SunEntity
{
    public sealed class CheckSunGravityResistSystem : IEcsRunSystem
    {
        private readonly EcsFilter<GravityResist, Energy> _gravityResistFilter = null;

        public void Run()
        {
            foreach (var i in _gravityResistFilter)
            {
                if (_gravityResistFilter.Get2(i).Current > 0f)
                    continue;

                _gravityResistFilter.GetEntity(i).Del<GravityResist>();
            }
        }
    }
}
