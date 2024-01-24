using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Develop.Runtime.Engine.UI.Weapon
{
    public class WeaponSlider : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private float _fadeDuration = 1f;
        [SerializeField] private CanvasGroup _canvasGroup;
        private CancellationTokenSource _tokenSource;

        private void OnEnable()
        {
            _slider.gameObject.SetActive(false);
        }

        public async UniTask SetSliderValue(float value)
        {
            _slider.SetValueWithoutNotify(value);

            if (Mathf.Approximately(value, 1f))
            {
                if (_tokenSource != null)
                {
                    return;
                }

                _tokenSource = new CancellationTokenSource();
                var token = CancellationTokenSource.CreateLinkedTokenSource(_tokenSource.Token, destroyCancellationToken).Token;
                try
                {
                    await FadeOut(_slider, _fadeDuration, token);
                }
                catch (Exception e)
                {
#if DEBUG
                    Debug.Log(e.Message);
#endif
                }
                finally
                {
                    _tokenSource.Dispose();
                    _tokenSource = null;
                }
            }
            else
            {
                _tokenSource?.Cancel();

                _slider.gameObject.SetActive(true);
                SetFillAlpha(1f);
            }
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