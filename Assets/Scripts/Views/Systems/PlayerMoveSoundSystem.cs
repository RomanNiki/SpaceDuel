using System.Collections.Generic;
using Extensions.MappingUnityToModel;
using Leopotam.Ecs;
using Model.Components;
using Model.Enums;
using Model.Extensions.Pause;
using Model.Unit.EnergySystems.Components;
using Model.Unit.Input.Components;
using Model.Unit.Input.Components.Events;

namespace Views.Systems
{
    public sealed class PlayerMoveSoundSystem : IPauseHandler, IEcsRunSystem
    {
        private readonly EcsFilter<UnityComponent<PlayerAudioComponent>, Team>.Exclude<NoEnergyBlock> _playerFilter;
        private readonly EcsFilter<UnityComponent<PlayerAudioComponent>, Team, NoEnergyBlock> _playerNoEnergyFilter;
        private readonly EcsFilter<InputAccelerateStartedEvent> _accelerateStartFilter;
        private readonly EcsFilter<InputAccelerateCanceledEvent> _accelerateCanceledFilter;
        private readonly EcsFilter<InputRotateStartedEvent> _rotateStartFilter;
        private readonly EcsFilter<InputRotateCanceledEvent> _rotateCanceledFilter;
        private readonly HashSet<TeamEnum> _playersAccelerating = new();
        private readonly HashSet<TeamEnum> _playersRotating = new();
        private bool _pause;

        public void Run()
        {
            foreach (var i in _accelerateStartFilter)
            {
                ref var accelerateTeam = ref _accelerateStartFilter.Get1(i);
                _playersAccelerating.Add(accelerateTeam.PlayerTeam);
            }

            foreach (var i in _accelerateCanceledFilter)
            {
                ref var accelerateTeam = ref _accelerateCanceledFilter.Get1(i);
                _playersAccelerating.Remove(accelerateTeam.PlayerTeam);
                foreach (var j in _playerFilter)
                {
                    ref var team = ref _playerFilter.Get2(j);
                    if (team.Value != accelerateTeam.PlayerTeam) continue;
                    ref var soundComponent = ref _playerFilter.Get1(j);
                    soundComponent.Value.StopAccelerateSound();
                }
            }

            foreach (var i in _playerNoEnergyFilter)
            {
                _playerNoEnergyFilter.Get1(i).Value.StopAccelerateSound();
                _playersAccelerating.Remove(_playerNoEnergyFilter.Get2(i).Value);
            }

            foreach (var i in _rotateStartFilter)
            {
                ref var rotateTeam = ref _rotateStartFilter.Get1(i);
                _playersRotating.Add(rotateTeam.PlayerTeam);
            }

            foreach (var i in _rotateCanceledFilter)
            {
                ref var rotateTeam = ref _rotateCanceledFilter.Get1(i);
                _playersRotating.Remove(rotateTeam.PlayerTeam);
            }
            
            foreach (var i in _playerNoEnergyFilter)
            {
                _playerNoEnergyFilter.Get1(i).Value.StopAccelerateSound();
                var team = _playerNoEnergyFilter.Get2(i).Value;
                _playersAccelerating.Remove(team);
                _playersRotating.Remove(team);
            }
            
            if (_pause)
            {
                return;
            }

            ProcessMoveSounds();
        }

        private void ProcessMoveSounds()
        {
            foreach (var j in _playerFilter)
            {
                ref var team = ref _playerFilter.Get2(j);
                ref var soundComponent = ref _playerFilter.Get1(j);
                if (_playersAccelerating.Contains(team.Value))
                {
                    soundComponent.Value.PlayAccelerateSound();
                }

                if (_playersRotating.Contains(team.Value) == false)
                {
                    continue;
                }

                soundComponent.Value.PlayRotateSound();
            }
        }

        public void SetPaused(bool isPaused)
        {
            _pause = isPaused;

            foreach (var j in _playerFilter)
            {
                ref var team = ref _playerFilter.Get2(j);
                if (_playersAccelerating.Contains(team.Value) == false) continue;
                ref var soundComponent = ref _playerFilter.Get1(j);
                soundComponent.Value.PlayAccelerateSound();
            }
        }
    }
}