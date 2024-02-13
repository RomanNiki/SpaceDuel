using System.Threading;
using _Project.Develop.Runtime.Engine.Infrastructure.Audio;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Sounds
{
    public class OneShotWithDelayPlayer : MonoBehaviour
    {
        [SerializeField] private AudioClip _audioClip;
        [Range(0f, 1f)] [SerializeField] private float _volumeFactor = 0.4f;
        [SerializeField] private GameAudioSource _audioSource;
        [SerializeField] private float _delay = 0.4f;
        private bool _isSoundPlay;
        
        private CancellationTokenSource _disableTokenSource;
        
        private void OnEnable()
        {
            _disableTokenSource ??= new CancellationTokenSource();
        }

        private void OnDisable()
        {
            _disableTokenSource?.Cancel();
            _disableTokenSource?.Dispose();
            _disableTokenSource = null;
            _isSoundPlay = false;
        }
        
        public async UniTaskVoid TryPlay()
        {
            if (_isSoundPlay) return;
            _audioSource.PlayOneShot(_audioClip, _volumeFactor);
            _isSoundPlay = true;
            await WaitEndOfRotationSound(_audioClip.length + _delay, _disableTokenSource.Token);
        }

        private async UniTask WaitEndOfRotationSound(float length, CancellationToken token = default)
        {
            await UniTask.WaitForSeconds(length, cancellationToken: token);
            _isSoundPlay = false;
        }
    }
}