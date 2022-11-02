using Models.Player;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Presenters
{
    public class PlayerGuiPresenter : MonoBehaviour
    {
        private readonly CompositeDisposable _disposable = new();
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private Slider _energySlider;

        [SerializeField] private Vector3 _offSet;
        [SerializeField] private PlayerPresenter _playerPresenter;

        private void Start()
        {
            _playerPresenter.Model.Health.Subscribe(OnHealthChanged);
            _playerPresenter.Model.Energy.Subscribe(OnEnergyChanged);
            _energySlider.maxValue = _playerPresenter.Model.Health.Value;
            _healthSlider.maxValue = _playerPresenter.Model.Energy.Value;
            Observable.EveryFixedUpdate().Subscribe(_ => { transform.position = _playerPresenter.Position + _offSet; })
                .AddTo(_disposable);
        }

        private void OnDestroy()
        {
            _disposable.Clear();
        }

        private void OnEnergyChanged(float value)
        {
            _energySlider.value = Mathf.Clamp(value, _energySlider.minValue, _energySlider.maxValue);
        }

        private void OnHealthChanged(float value)
        {
            _healthSlider.value = Mathf.Clamp(value, _energySlider.minValue, _energySlider.maxValue);
        }
    }
}