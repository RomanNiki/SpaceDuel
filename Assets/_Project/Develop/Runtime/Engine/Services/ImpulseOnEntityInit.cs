using _Project.Develop.Runtime.Core.Services.Random;
using _Project.Develop.Runtime.Engine.Providers;
using Cinemachine;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Services
{
    [RequireComponent(typeof(EntityProvider))]
    [RequireComponent(typeof(CinemachineImpulseSource))]
    public class ImpulseOnEntityInit : MonoBehaviour
    {
        private static readonly Vector2 ImpulseVelocityFactor = new(-2, 2);
        private EntityProvider _entityProvider;
        private CinemachineImpulseSource _impulseSource;
        private readonly IRandom _random = FastRandom.Singleton;
        private void Awake()
        {
            _entityProvider = GetComponent<EntityProvider>();
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

        private Vector3 GetVelocity()
        {
            var x = _random.Range(ImpulseVelocityFactor.x, ImpulseVelocityFactor.y);
            var y = _random.Range(ImpulseVelocityFactor.x, ImpulseVelocityFactor.y);
            return new Vector3(x, y, 0f);
        }
    }
}