using Core.Characteristics.EnergyLimits.Components;
using Core.Extensions;
using Core.Input.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;
using UnityEngine;

namespace Core.Characteristics.EnergyLimits.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    
    public sealed class MoveDischargeSystem : UpdateSystem
    {
        private const float MIN_ROTATION_FOR_DISCHARGE = 0.2f;
        private Filter _filter;
        private Stash<RotateDischargeAmount> _rotateDischargeAmountPool;
        private Stash<AccelerateDischargeAmount> _accelerateDischargeAmountPool;
        private Stash<InputMoveData> _inputMovePool;

        public override void OnAwake()
        {
            _filter = World.Filter.With<InputMoveData>().With<AccelerateDischargeAmount>().With<RotateDischargeAmount>()
                .Without<NoEnergyBlock>().Build();
            _accelerateDischargeAmountPool = World.GetStash<AccelerateDischargeAmount>();
            _rotateDischargeAmountPool = World.GetStash<RotateDischargeAmount>();
            _inputMovePool = World.GetStash<InputMoveData>();
        }

        public override void OnUpdate(float deltaTime)
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

                CreateDischargeRequest(dischargeAmount, entity);
            }
        }

        private void CreateDischargeRequest(float dischargeAmount, Entity entity)
        {
            if (dischargeAmount <= 0f) return;
            World.SendMessage(new DischargeRequest{Value = dischargeAmount, Entity = entity});
        }
    }
}