using _Project.Develop.Runtime.Core.Movement.Components;
using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Core.Movement.Aspects
{
    public struct TransformAspect : IAspect, IFilterExtension 
    {
        public ref Position Position => ref _position.Get(Entity);
        public ref Rotation Rotation => ref _rotation.Get(Entity);
    
        private Stash<Position> _position;
        private Stash<Rotation> _rotation;

        public void OnGetAspectFactory(World world)
        {
            _position = world.GetStash<Position>();
            _rotation = world.GetStash<Rotation>();
        }

        public Entity Entity { get; set; }
        public FilterBuilder Extend(FilterBuilder rootFilter) => rootFilter.With<Position>().With<Rotation>();
    }
}