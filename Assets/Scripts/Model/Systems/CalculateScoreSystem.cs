using Leopotam.Ecs;
using Model.Components;
using Model.Components.Requests;
using Model.Components.Tags;
using Model.Components.Unit.MoveComponents.Input;

namespace Model.Systems
{
    public sealed class CalculateScoreSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Team, EntityDestroyRequest, PlayerTag> _filter;
        private readonly EcsFilter<Score, Team> _scoreFilter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var team = ref _filter.Get1(i);
                foreach (var j in _scoreFilter)
                {
                    ref var scoreTeam = ref _scoreFilter.Get2(j);
                    if (scoreTeam.Value != team.Value) continue;
                    ref var score = ref _scoreFilter.Get1(j);
                    ref var scoreEntity = ref _scoreFilter.GetEntity(j);
                    score.Value++;
                    scoreEntity.Get<ViewUpdateRequest>();
                }
            }
        }
    }
}