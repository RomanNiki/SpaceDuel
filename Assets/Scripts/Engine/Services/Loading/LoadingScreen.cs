using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Engine.Common;
using Engine.Services.Loading.LoadingOperations;
using TMPro;
using UnityEngine;

namespace Engine.Services.Loading
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _loader;
        [SerializeField] private float _fadeTime;
        [SerializeField] private TMP_Text _loaderText;
        [SerializeField] private BlockLoadingSlider _progressFill;
        [SerializeField] private float _barSpeed;
        private float _targetProgress;
        private bool _isProgress;
        private CancellationToken _token;
        private CancellationTokenSource _fadeCancellationTokenSource;

        private async void Awake()
        {
            await Toggle(true);
        }

        private void OnEnable()
        {
            _token = gameObject.GetCancellationTokenOnDestroy();
        }

        public async UniTask Load(Queue<ILoadingOperation> loadingOperations)
        {
            try
            {
                UpdateProgressBar(_token).Forget();

                foreach(var operation in loadingOperations)
                {
                    ResetFill();
                    _loaderText.text = operation.Description;
                    await operation.Load(OnProgress).AttachExternalCancellation(_token);
                    await WaitForBarFill().AttachExternalCancellation(_token);
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
            finally
            {
                await Toggle(false);
            }
        }

        private async UniTask Toggle(bool on, bool instant = false)
        {
            if (_fadeCancellationTokenSource != null)
            {
                _fadeCancellationTokenSource?.Cancel();
                _fadeCancellationTokenSource?.Dispose();
            }

            try
            {
                _fadeCancellationTokenSource = new CancellationTokenSource();
                _fadeCancellationTokenSource.AddTo(_token);
                await _loader.Fade(on ? 1 : 0, instant ? 0 : _fadeTime, _fadeCancellationTokenSource.Token);
                _loader.enabled = on;
            }
            catch when (_token.IsCancellationRequested)
            {
            }
            finally
            {
                _fadeCancellationTokenSource?.Dispose();
                _fadeCancellationTokenSource = null;
            }
        }

        private void ResetFill()
        {
            _progressFill.ChangeValue(0);
            _targetProgress = 0;
        }

        private void OnProgress(float progress)
        {
            _targetProgress = progress;
        }

        private async UniTask WaitForBarFill()
        {
            while (_progressFill.Value < _targetProgress)
            {
                await UniTask.Yield(cancellationToken: _token);
            }

            await UniTask.Delay(TimeSpan.FromSeconds(0.15f), cancellationToken: _token);
        }

        private async UniTask UpdateProgressBar(CancellationToken token = default)
        {
            while (token.IsCancellationRequested == false)
            {
                if (_loader.enabled == false)
                {
                    return;
                }

                if (_progressFill.Value < _targetProgress)
                {
                    _progressFill.ChangeValue(_progressFill.Value + UnityEngine.Time.deltaTime * _barSpeed);
                }

                await UniTask.Yield();
            }
        }
    }
}