using Core.Characteristics.EnergyLimits.Components;
using UnityEngine;

namespace Engine.Providers
{
    public class SunEntity : GravityPointEntity
    {
        [SerializeField] private float _chargeAmount;
        
        protected override void OnInit()
        {
            base.OnInit();
            SetData(new ChargeContainer(){Value = _chargeAmount});
        }
    }
}