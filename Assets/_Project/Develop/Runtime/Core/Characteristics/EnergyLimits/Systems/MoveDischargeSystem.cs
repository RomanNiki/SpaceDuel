using _Project.Develop.Runtime.Core.Characteristics.EnergyLimits.Components;
using _Project.Develop.Runtime.Core.Common;
using _Project.Develop.Runtime.Core.Extensions;
using _Project.Develop.Runtime.Core.Input.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;
using UnityEngine;

namespace _Project.Develop.Runtime.Core.Characteristics.EnergyLimits.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif

    public sealed class MoveDischargeSystem : UpdateSystem
    {
      
        private Filter _filter;
        private Stash<RotateDischargeSpeed> _rotateDischargeAmountPool;
        private Stash<AccelerateDischargeSpeed> _accelerateDischargeAmountPool;
        private Stash<InputMoveData> _inputMovePool;
        private Stash<Energy> _energyPool;

        public override void OnAwake()
        {
            _filter = World.Filter.With<InputMoveData>().With<AccelerateDischargeSpeed>().With<RotateDischargeSpeed>()
                .With<Energy>().Build();
            _accelerateDischargeAmountPool = World.GetStash<AccelerateDischargeSpeed>();
            _energyPool = World.GetStash<Energy>();
            _rotateDischargeAmountPool = World.GetStash<RotateDischargeSpeed>();
            _inputMovePool = World.GetStash<InputMoveData>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var energy = ref _energyPool.Get(entity);
                if (energy.HasEnergy == false)
                {
                    continue;
                }

                var input = _inputMovePool.Get(entity);
                var dischargeAmount = 0f;
                if (input.Accelerate)
                {
                    dischargeAmount += _accelerateDischargeAmountPool.Get(entity).Value;
                }

                if (Mathf.Abs(input.Rotation) >= GameConfig.MinRotationForDischarge)
                {
                    dischargeAmount += _rotateDischargeAmountPool.Get(entity).Value;
                }

                CreateDischargeRequest(dischargeAmount * deltaTime, entity);
            }
        }

        private void CreateDischargeRequest(float dischargeAmount, Entity entity)
        {
            if (dischargeAmount <= 0f) return;
            World.SendMessage(new DischargeRequest { Value = dischargeAmount, Entity = entity });
        }
    }
}