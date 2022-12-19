using Leopotam.Ecs;
using Model.Components.Extensions;
using Model.Components.Extensions.Interfaces;

namespace Model.Components.Tags
{
    public struct PlayerTag : IEcsIgnoreInFilter, IGameEntityTag
    {
    }
}