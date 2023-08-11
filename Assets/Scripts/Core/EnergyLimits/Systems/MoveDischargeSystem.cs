using Core.EnergyLimits.Components;
using Core.Input.Components;
using Core.Player.Components;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.EnergyLimits.Systems
{
    public class MoveDischargeSystem : ISystem
    {
        private const float MIN_ROTATION_FOR_DISCHARGE = 0.2f;
        private Filter _filter;
        private Stash<DischargeRequest> _dischargeRequestPool;
        private Stash<RotateDischargeAmount> _rotateDischargeAmountPool;
        private Stash<AccelerateDischargeAmount> _accelerateDischargeAmountPool;
        private Stash<InputMoveData> _inputMovePool;
        public World World { get; set; }
        
        public void OnAwake()
        {
            _filter = World.Filter.With<InputMoveData>().With<AccelerateDischargeAmount>().With<RotateDischargeAmount>()
                .Without<NoEnergyBlock>();
            _dischargeRequestPool = World.GetStash<DischargeRequest>();
            _accelerateDischargeAmountPool = World.GetStash<AccelerateDischargeAmount>();
            _rotateDischargeAmountPool = World.GetStash<RotateDischargeAmount>();
            _inputMovePool = World.GetStash<InputMoveData>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                var input = _inputMovePool.Get(entity);
                var dischargeAmount = 0f;
                if (input.Accelerate)
                {
                    dischargeAmount += _accelerateDischargeAmountPool.Get(entity).Value;
                }

                if (Mathf.Abs(input.Rotation) >= MIN_ROTATION_FOR_DISCHARGE)
                {
                    dischargeAmount += _rotateDischargeAmountPool.Get(entity).Value;
                }

                TryAddDischargeRequest(dischargeAmount, entity);
            }
        }

        private void TryAddDischargeRequest(float dischargeAmount, Entity entity)
        {
            if (dischargeAmount <= 0f) return;

            if (_dischargeRequestPool.Has(entity) == false)
            {
                _dischargeRequestPool.Add(entity);
            }

            _dischargeRequestPool.Get(entity).Value += dischargeAmount;
        }

        public void Dispose()
        {
        }
    }
}