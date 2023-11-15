using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.UI.Loading
{
    public class DotsTextAnimation : MonoBehaviour
    {
        [SerializeField] private TMP_Text _textHolder;
        [SerializeField] private string _text;
        [SerializeField] private int _maxDotsCount;
        [SerializeField] private float _updateRateSec;


        private void Start()
        {
            UpdateDots(gameObject.GetCancellationTokenOnDestroy()).Forget();
        }

        private async UniTaskVoid UpdateDots(CancellationToken cancellationToken)
        {
            var dotsCount = 0;
            try
            {
                while (cancellationToken.IsCancellationRequested == false)
                {
                    dotsCount = (dotsCount + 1) % (_maxDotsCount + 1);

                    var dots = new string('.', dotsCount);
                    _textHolder.text = _text + dots;
                    await UniTask.Delay(TimeSpan.FromSeconds(_updateRateSec), cancellationToken: cancellationToken);
                }
            }
            catch when (cancellationToken.IsCancellationRequested)
            {
                // ignored
            }
        }
    }
}