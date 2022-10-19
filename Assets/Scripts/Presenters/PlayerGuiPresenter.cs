using TMPro;
using UniRx;
using UnityEngine;

namespace Presenters
{
    public class PlayerGuiPresenter : MonoBehaviour
    {
        private readonly CompositeDisposable _disposable = new();
        [SerializeField] private TMP_Text _healthText;
        [SerializeField] private string _healthTextTemplate;

        [SerializeField] private TMP_Text _energyText;
        [SerializeField] private string _energyTextTemplate;

        [SerializeField] private Vector3 _offSet;
        [SerializeField] private PlayerPresenter _playerPresenter;

        private void Start()
        {
            _playerPresenter.HealthProperty.Subscribe(OnHealthChanged);
            _playerPresenter.EnergyProperty.Subscribe(OnEnergyChanged);
            Observable.EveryFixedUpdate().Subscribe(_ =>
            {
                transform.position = _playerPresenter.Position + _offSet;
            }).AddTo(_disposable);
        }

        private void OnDestroy()
        {
            _disposable.Clear();
        }

        private void OnEnergyChanged(float value)
        {
            _energyText.text = value.ToString(_energyTextTemplate);
        }

        private void OnHealthChanged(float value)
        {
            _healthText.text = value.ToString(_healthTextTemplate);
        }
    }
}