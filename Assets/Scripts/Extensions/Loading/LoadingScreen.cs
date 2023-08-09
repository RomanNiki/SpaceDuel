using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Extensions.Loading.LoadingOperations;
using TMPro;
using UnityEngine;

namespace Extensions.Loading
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _loader;
        [SerializeField] private float _fadeTime;
        [SerializeField] private TMP_Text _loaderText, _errorText;
        [SerializeField] private BlockLoadingSlider _progressFill;
        [SerializeField] private float _barSpeed;
        private CancellationTokenSource _cancellationTokenSource;
        private TweenerCore<float, float, FloatOptions> _tween;
        private float _targetProgress;
        private bool _isProgress;

        private async void Awake()
        {
            DontDestroyOnLoad(this);
            await Toggle(true);
        }

        private void OnDisable()
        {
            _cancellationTokenSource?.Cancel();
        }

        private void OnDestroy()
        {
            _cancellationTokenSource?.Cancel();
            _tween?.Kill();
        }

        public async UniTask Load(Queue<ILoadingOperation> loadingOperations)
        {
            try
            {
                _cancellationTokenSource = new CancellationTokenSource();
                UpdateProgressBar(_cancellationTokenSource.Token).Forget();

                foreach(var operation in loadingOperations)
                {
                    ResetFill();
                    _loaderText.text = operation.Description;
                    await operation.Load(OnProgress).AttachExternalCancellation(_cancellationTokenSource.Token);
                    await WaitForBarFill().AttachExternalCancellation(_cancellationTokenSource.Token);
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
            finally
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = null;
                await Toggle(false);
            }
        }

        private async UniTask Toggle(bool on, bool instant = false)
        {
            _tween?.Kill();
            _tween = _loader.DOFade(on ? 1 : 0, instant ? 0 : _fadeTime);
            await _tween.AsyncWaitForCompletion();
            _loader.enabled = on;
        }

        public void ShowError(string error)
        {
            _errorText.text = error;
            _errorText.DOFade(1, _fadeTime).OnComplete(() => { _errorText.DOFade(0, _fadeTime).SetDelay(1); });
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
                await UniTask.Delay(1);
            }

            await UniTask.Delay(TimeSpan.FromSeconds(0.15f));
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
                    _progressFill.ChangeValue(_progressFill.Value + Time.deltaTime * _barSpeed);
                }

                await UniTask.Yield();
            }
        }
    }
}