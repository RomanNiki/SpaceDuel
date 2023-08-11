using Core.EnergyLimits.Components;
using Core.Input.Components;
using Core.Movement.Components;
using Core.Player.Components;
using ScriptableObjects;
using UnityEngine;
using Views;
using Views.Moveable;
using ViewObject = Core.Extensions.Components.ViewObject;

namespace Entities
{
    public sealed class SpaceShipEntityProvider : EntityProvider
    {
        [SerializeField] private ShipSettings _shipSettings;
        [SerializeField] private Team _team;
        [SerializeField] private Rigidbody _rigidbody;

        protected override void OnInit()
        {
            SetData(new PlayerTag());
            SetData(new InputMoveData());
            AddMove();
            AddStats();
            var moveStrategy = new RigidBodyMoveStrategy(_rigidbody);
            SetData(new ViewObject { Value = new UnityViewObject(transform, moveStrategy, this) });
        }

        protected override void OnDispose()
        {
            Destroy(gameObject);
        }

        private void AddStats()
        {
            SetData(new Health(_shipSettings.MaxHealth));
            SetData(new Energy() { MaxValue = _shipSettings.MaxEnergy, Value = _shipSettings.MaxEnergy });
            SetData(new AccelerateDischargeAmount() { Value = _shipSettings.AccelerateDischargeAmount });
            SetData(new RotateDischargeAmount() { Value = _shipSettings.RotateDischargeAmount });
        }

        private void AddMove()
        {
            SetData(new Velocity());
            SetData(new Position { Value = transform.position });
            SetData(new Rotation { Value = transform.eulerAngles.z });
            SetData(_shipSettings.Mass);
            SetData(_shipSettings.Friction);
            SetData(_shipSettings.RotationSpeed);
            SetData(_shipSettings.Speed);
            SetData(_team);
        }
    }
}