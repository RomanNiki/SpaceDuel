using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Services
{
    public class AccelerateEffectController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;

        public void Play()
        {
            if (_particleSystem.isPlaying) return;
            _particleSystem.Play();
        }

        public void Stop()
        {
            if (_particleSystem.isStopped) return;
            _particleSystem.Stop();
        }
    }
}