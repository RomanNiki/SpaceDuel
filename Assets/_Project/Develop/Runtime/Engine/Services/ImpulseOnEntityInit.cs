using _Project.Develop.Runtime.Engine.Providers;
using Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Develop.Runtime.Engine.Services
{
    [RequireComponent(typeof(CinemachineImpulseSource))]
    public class ImpulseOnEntityInit : MonoBehaviour
    {
        private static readonly Vector2 ImpulseVelocityFactor = new(-2, 2);
        [SerializeField] private EntityProvider _entityProvider;
        private CinemachineImpulseSource _impulseSource;

        private void Awake()
        {
            _impulseSource = GetComponent<CinemachineImpulseSource>();
        }

        private void OnEnable()
        {
            _entityProvider.EntityInit += OnEntityInit;
        }

        private void OnDisable()
        {
            _entityProvider.EntityInit -= OnEntityInit;
        }

        private void OnEntityInit()
        {
            _impulseSource.GenerateImpulseAtPositionWithVelocity(transform.position, GetVelocity());
        }

        private static Vector3 GetVelocity()
        {
            var x = Random.Range(ImpulseVelocityFactor.x, ImpulseVelocityFactor.y);
            var y = Random.Range(ImpulseVelocityFactor.x, ImpulseVelocityFactor.y);
            return new Vector3(x, y, 0f);
        }
    }
}