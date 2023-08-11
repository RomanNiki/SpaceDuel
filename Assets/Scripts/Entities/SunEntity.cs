using Core.EnergyLimits.Components;
using UnityEngine;

namespace Entities
{
    public class SunEntity : GravityPointEntity
    {
        [SerializeField] private ChargeRequest _chargeAmount;
        
        protected override void OnInit()
        {
            base.OnInit();
            SetData(new ChargeContainer(){ChargeRequest = _chargeAmount});
        }
    }
}