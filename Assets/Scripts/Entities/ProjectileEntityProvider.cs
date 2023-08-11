using UnityEngine;
using Views;
using Views.Moveable;
using ViewObject = Core.Extensions.Components.ViewObject;

namespace Entities
{
    public class ProjectileEntityProvider : PoolableEntityProvider
    {
        [SerializeField] private Rigidbody _rigidbody;

        protected override void OnInit()
        {
            var moveStrategy = new RigidBodyMoveStrategy(_rigidbody);
            SetData(new ViewObject { Value = new UnityViewObject(transform, moveStrategy, this) });
        }
    }
}