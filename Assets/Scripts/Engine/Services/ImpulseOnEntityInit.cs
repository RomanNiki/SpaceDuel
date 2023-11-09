using Cinemachine;
using Engine.Providers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Engine.Services
{
    [RequireComponent(typeof(CinemachineImpulseSource))]
    public class ImpulseOnEntityInit : MonoBehaviour
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
            var x = Random.Range(-3f, 3f);
            var y = Random.Range(-3f, 3f);
            return new Vector3(x, y, 0f);
        }
    }
}