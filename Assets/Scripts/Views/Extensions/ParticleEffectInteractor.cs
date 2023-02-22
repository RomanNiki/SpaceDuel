using UnityEngine;

namespace Views.Extensions
{
    public class ParticleEffectInteractor : EffectInteractor
    {
        [SerializeField] private ParticleSystem _particleSystem;
        
        public override void SetPause(bool isPause)
        {
            if (isPause)
            {
                _particleSystem.Pause();
            }
            else
            {
                Play();
            }
        }

        public override void Stop()
        {
            _particleSystem.Stop();
        }

        public override void Play()
        {
            _particleSystem.Play();
        }
    }
}