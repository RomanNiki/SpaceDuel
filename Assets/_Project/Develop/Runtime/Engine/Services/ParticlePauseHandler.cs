using _Project.Develop.Runtime.Core.Services.Pause;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace _Project.Develop.Runtime.Engine.Services
{
    public class ParticlePauseHandler : MonoBehaviour, IPauseHandler
    {
        [SerializeField] private ParticleSystem _particleSystem;
        private IPauseService _pauseService;

        [Inject]
        public void Construct(IPauseService pauseService)
        {
            _pauseService = pauseService;
            _pauseService.AddPauseHandler(this);
        }

        private void OnEnable()
        {
            if (_particleSystem.isStopped == false)
            {
                _particleSystem.Play();
            }
        }

        private void OnDestroy()
        {
            _pauseService.RemovePauseHandler(this);
        }

        public async UniTask SetPaused(bool isPaused)
        {
            if (isPaused)
            {
                _particleSystem.Pause();
            }
            else
            {
                _particleSystem.Play();
                if (_particleSystem.isStopped == false)
                {
                    _particleSystem.Stop();
                }
            }
        }
    }
}