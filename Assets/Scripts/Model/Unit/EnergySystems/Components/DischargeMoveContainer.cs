using System;
using Model.Unit.EnergySystems.Components.Requests;

namespace Model.Unit.EnergySystems.Components
{
    [Serializable]
    public struct DischargeMoveContainer
    {
        public DischargeRequest DischargeRequest;
    }
}