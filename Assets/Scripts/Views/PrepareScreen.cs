using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Views
{
    public class PrepareScreen : MonoBehaviour
    {
        [SerializeField] private TMP_Text _tmpText;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _fadeTime;
        private bool _live;

        private void OnEnable()
        {
            _canvasGroup.alpha = 1f;
            _live = true;
        }

        private void OnDisable()
        {
            _live = false;
        }

        public void SetText(string text)
        {
            _tmpText.text = text;
        }

        public async UniTask Disappear()
        {
            var elapsedTime = 0.0f;
            while (elapsedTime < _fadeTime && _live)
            {
                elapsedTime += Time.deltaTime;
                var alpha = 1.0f - Mathf.Clamp01(elapsedTime / _fadeTime);
                _canvasGroup.alpha = alpha;
                await UniTask.Yield();
            }
        }
    }
}