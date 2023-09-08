using Engine.Movement.Strategies;
using Engine.Views;
using UnityEngine;
using ViewObject = Core.Views.Components.ViewObject;

namespace Engine.Providers
{
    public class ProjectileEntityProvider : PoolItemEntityProvider
    {
        [SerializeField] private Rigidbody _rigidbody;

        protected override void OnInit()
        {
            var moveStrategy = new RigidBodyMoveStrategy(_rigidbody);
            SetData(new ViewObject { Value = new UnityViewObject(this, moveStrategy) });
        }
    }
}