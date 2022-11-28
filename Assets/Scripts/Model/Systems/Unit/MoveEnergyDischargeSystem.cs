using Leopotam.Ecs;
using Model.Components;
using Model.Components.Requests;
using Model.Components.Tags;
using Model.Components.Unit;
using Model.Components.Unit.MoveComponents.Input;
using UnityEngine;

namespace Model.Systems.Unit
{
    public sealed class MoveEnergyDischargeSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerTag, InputMoveData, DischargeMoveContainer, DischargeRotateContainer>.Exclude<NoEnergyBlock> _player = null;

        public void Run()
        {
            foreach (var i in _player)
            {
                ref var inputData = ref _player.Get2(i);
                ref var entity = ref _player.GetEntity(i);

                if (inputData.Accelerate)
                {
                    ref var discharge = ref _player.Get3(i).DischargeRequest.Value;
                    entity.Get<DischargeRequest>().Value += discharge;
                }

                if (Mathf.Abs(inputData.Rotation) > 0.02f)
                {
                    ref var discharge = ref _player.Get4(i).DischargeRequest.Value;
                    entity.Get<DischargeRequest>().Value += discharge;
                }
            }
        }
    }
}