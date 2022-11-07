using System;
using Messages;
using Models.Player;
using TMPro;
using Zenject;

namespace Models
{
    public class GameScore :  IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly Settings _settings;
        private int _blueScore;
        private int _redScore;

        public GameScore(Settings settings, SignalBus signalBus)
        {
            _signalBus = signalBus;
            _settings = settings;
        }
        
        public void Initialize()
        {
            _signalBus.Subscribe<PlayerDiedMessage>(OnPlayerDied);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<PlayerDiedMessage>(OnPlayerDied);
        }
        
        private void OnPlayerDied(PlayerDiedMessage message)
        {
            switch (message.Team)
            {
                case Team.Blue:
                    _redScore++;
                    ChangeScore(_settings.RedScore, _redScore);
                    break;
                case Team.Red:
                    _blueScore++;
                    ChangeScore(_settings.BlueScore, _blueScore);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ChangeScore(TMP_Text text, int score)
        {
            text.text = score.ToString(_settings.ScoreTextFormat);
        }
        
        [Serializable]
        public class Settings
        {
            public TMP_Text BlueScore;
            public TMP_Text RedScore;
            public string ScoreTextFormat;
        }
    }
}