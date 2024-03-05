using System;
using System.Threading;
using _Project.Develop.Runtime.Engine.UI.Statistics.Sliders;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.UI.Weapon
{
    public class FilledSliderDisappear : MonoBehaviour
    {
        [SerializeField] private BaseStatisticSlider _slider;
        [SerializeField] private float _fadeDuration = 1f;
        [SerializeField] private CanvasGroup _canvasGroup;
        private CancellationTokenSource _tokenSource;

        private void OnEnable()
        {
            _slider.SliderValueChanged += OnSliderValueChanged;
        }

        private void OnDisable()
        {
            _slider.SliderValueChanged -= OnSliderValueChanged;
        }

        private void OnSliderValueChanged(float value)
        {
            if (Mathf.Approximately(MathF.Round(value, 1), 1f))
            {
                StartFadeOut().Forget();
            }
            else
            {
                FadeIn();
            }
        }

        private void FadeIn()
        {
            _tokenSource?.Cancel();
            _slider.gameObject.SetActive(true);
            SetFillAlpha(1f);
        }

        private async UniTaskVoid StartFadeOut()
        {
            if (_tokenSource != null)
                return;

            var token = CreateLinkedCancellationToken();
            try
            {
                await FadeOut(_slider, _fadeDuration, token);
            }
            catch when (token.IsCancellationRequested)
            {
               Debug.Log("Cancelled");
            }
            finally
            {
                _tokenSource?.Dispose();
                _tokenSource = null;
            }
        }

        private CancellationToken CreateLinkedCancellationToken()
        {
            _tokenSource = new CancellationTokenSource();
            var token = CancellationTokenSource.CreateLinkedTokenSource(_tokenSource.Token, destroyCancellationToken)
                .Token;
            return token;
        }

        private void SetFillAlpha(float alpha)
        {
            _canvasGroup.alpha = alpha;
        }

        private async UniTask FadeOut(Component slider, float fadeDuration, CancellationToken token = default)
        {
            try
            {
                var elapsedTime = 0f;
                while (elapsedTime < fadeDuration)
                {
                    SetFillAlpha(Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration));
                    elapsedTime += Time.deltaTime;
                    await UniTask.Yield();
                }

                slider.gameObject.SetActive(false);
            }
            catch when (token.IsCancellationRequested)
            {
            }
        }
    }
}