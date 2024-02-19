using _Project.Develop.Runtime.Engine.Sounds.Interfaces;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Sounds
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(ShotAudioSource))]
    public abstract class PitchInitializer<TSoundAction> : MonoBehaviour
    where TSoundAction : MonoBehaviour, ISoundAction
    {
        private PlayPitchChanger _pitchChanger;

        private void Awake()
        {
            var audioSource = GetComponent<AudioSource>();
            var shotAudioSource = GetComponent<TSoundAction>();
            _pitchChanger = new PlayPitchChanger(shotAudioSource, audioSource);
        }
    }
}