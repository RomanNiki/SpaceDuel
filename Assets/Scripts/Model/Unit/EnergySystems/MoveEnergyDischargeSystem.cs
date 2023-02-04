using Leopotam.Ecs;
using Model.Extensions;
using Model.Unit.EnergySystems.Components;
using Model.Unit.EnergySystems.Components.Requests;
using Model.Unit.Input.Components;
using Model.Unit.Movement.Components.Tags;
using UnityEngine;

namespace Model.Unit.EnergySystems
{
    public sealed class MoveEnergyDischargeSystem : PauseHandlerDefaultRunSystem
    {
        private readonly 
            EcsFilter<PlayerTag, InputMoveData, DischargeMoveContainer, DischargeRotateContainer>.Exclude<NoEnergyBlock>
            _moveDischargeFilter = null;

        protected override void Tick()
        {
            foreach (var i in _moveDischargeFilter)
            {
                ref var inputData = ref _moveDischargeFilter.Get2(i);
                ref var entity = ref _moveDischargeFilter.GetEntity(i);

                if (inputData.Accelerate)
                {
                    ref var discharge = ref _moveDischargeFilter.Get3(i).DischargeRequest.Value;
                    entity.Get<DischargeRequest>().Value += discharge;
                }

                if (Mathf.Abs(inputData.Rotation) < 0.02f) continue;
                ref var dischargeRotation = ref _moveDischargeFilter.Get4(i).DischargeRequest.Value;
                entity.Get<DischargeRequest>().Value += dischargeRotation;
            }
        }
    }
}