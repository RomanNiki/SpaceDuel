using Leopotam.Ecs;
using Model.Components;
using Model.Components.Requests;
using Model.Scores.Components;
using TMPro;

namespace Views.Systems.Update
{
    public sealed class UpdateScoreViewSystem : IEcsRunSystem
    {
        private readonly EcsFilter<UnityComponent<TMP_Text>, Score, ViewUpdateRequest> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var textComponent = ref _filter.Get1(i);
                ref var score = ref _filter.Get2(i);
                textComponent.Value.text = score.Value.ToString();
            }
        }
    }
}