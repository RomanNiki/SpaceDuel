using Core.Movement.Components;
using Core.Sun.Components;
using UnityEngine;

namespace Entities
{
    public class GravityPointEntity : EntityProvider
    {
        [SerializeField] private Mass _mass;
        [SerializeField] private GravityPoint _gravityPoint;

        protected override void OnInit()
        {
            SetData(_mass);
            SetData(_gravityPoint);
            SetData(new Position { Value = transform.position });
            SetData(new Rotation { Value = transform.eulerAngles.z });
        }
        
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            var position = transform.position;
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(position, _gravityPoint.OuterRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(position, _gravityPoint.InnerRadius);
        }
#endif
    }
}