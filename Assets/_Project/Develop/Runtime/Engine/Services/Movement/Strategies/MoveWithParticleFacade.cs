using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Services.Movement.Strategies
{
    public class MoveWithParticleFacade : IMoveStrategy
    {
        private readonly IMoveStrategy _moveStrategy;
        private readonly ParticleSystem _particleSystem;
        private Vector3 _position;
        private bool _paused;
        
        public MoveWithParticleFacade(IMoveStrategy moveStrategy, ParticleSystem particleSystem)
        {
            _moveStrategy = moveStrategy;
            _particleSystem = particleSystem;
        }

        public void MoveTo(Vector3 position)
        {
            const float particleMoveMaxDistance = 25f;
            if (Vector3.Distance(_position, position) > particleMoveMaxDistance)
            {
                _paused = true;
                _particleSystem.Pause();
            }
            
            _position = position;
            _moveStrategy.MoveTo(position);

            if (_particleSystem.isStopped || _paused == false) return;
            _paused = false;
            _particleSystem.Play();
        }
    }
}