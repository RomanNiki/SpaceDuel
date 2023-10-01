using Core.Common;
using Core.Init.Systems;
using Scellecs.Morpeh.Addons.Feature;

namespace Core.Init
{
    public class InitFeature : UpdateFeature
    {
        private readonly PlayersSpawnPoints _playerPoints;

        public InitFeature(PlayersSpawnPoints playerPoints)
        {
            _playerPoints = playerPoints;
        }

        protected override void Initialize()
        {
            AddInitializer(new PlayerInitSystem(_playerPoints));
        }
    }
}