using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Extensions.Loading.LoadingOperations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Extensions.Loading
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _loader;
        [SerializeField] private float _fadeTime;
        [SerializeField] private TMP_Text _loaderText, _errorText;
        [SerializeField] private Slider _progressFill;
        [SerializeField] private float _barSpeed;

        private TweenerCore<float, float, FloatOptions> _tween;
        private float _targetProgress;
        private bool _isProgress;

        private async void Awake()
        {
            DontDestroyOnLoad(this);
            await Toggle(true);
        }

        private void OnDestroy()
        {
            _tween?.Kill();
        }

        public async UniTask Load(Queue<ILoadingOperation> loadingOperations)
        {
            StartCoroutine(UpdateProgressBar());
            foreach (var operation in loadingOperations)
            {
                ResetFill();
                _loaderText.text = operation.Description;
                await operation.Load(OnProgress);
                await WaitForBarFill();
            }

            await Toggle(false);
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
            _progressFill.value = 0;
            _targetProgress = 0;
        }

        private void OnProgress(float progress)
        {
            _targetProgress = progress;
        }

        private async UniTask WaitForBarFill()
        {
            while (_progressFill.value < _targetProgress)
            {
                await UniTask.Delay(1);
            }

            await UniTask.Delay(TimeSpan.FromSeconds(0.15f));
        }

        private IEnumerator UpdateProgressBar()
        {
            while (_loader.enabled)
            {
                if (_progressFill.value < _targetProgress)
                {
                    _progressFill.value += Time.deltaTime * _barSpeed;
                }

                yield return null;
            }
        }
    }
}