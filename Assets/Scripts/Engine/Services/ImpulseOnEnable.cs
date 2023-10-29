using Cinemachine;
using Engine.Providers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Engine.Services
{
    [RequireComponent(typeof(CinemachineImpulseSource))]
    public class ImpulseOnEnable : MonoBehaviour
    {
        private CinemachineImpulseSource _impulseSource;
        [SerializeField] private EntityProvider _entityProvider;
        

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
            var x = Random.Range(-2f, 2f);
            var y = Random.Range(-2f, 2f);
            return new Vector3(x, y, 0f);
        }
    }
}