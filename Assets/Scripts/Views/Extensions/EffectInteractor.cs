using UnityEngine;

namespace Views.Extensions
{
    public abstract class EffectInteractor : MonoBehaviour
    {
        public abstract void SetPause(bool isPause);
        public abstract void Stop();
        public abstract void Play();
    }
}