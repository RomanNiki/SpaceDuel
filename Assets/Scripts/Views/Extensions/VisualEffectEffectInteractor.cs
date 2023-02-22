using UnityEngine;
using UnityEngine.VFX;

namespace Views.Extensions
{
    public class VisualEffectEffectInteractor : EffectInteractor
    {
        [SerializeField] private VisualEffect _visualEffect;
        
        public override void SetPause(bool isPause)
        {
            _visualEffect.pause = isPause;
        }

        public override void Stop()
        {
            _visualEffect.Stop();
        }

        public override void Play()
        {
           _visualEffect.Play();
        }
    }
}