using System;
using UnityEngine;
using Zenject;

namespace Models.Player
{
    public class PlayerMover : Mover, IFixedTickable
    {
        private readonly Settings _settings;
        private readonly PlayerInputRouter _inputRouter;
        private readonly PlayerModel _player;

        public PlayerMover(Settings settings, PlayerModel player, PlayerInputRouter inputRouter, Camera camera) : base(player, camera)
        {
            _settings = settings;
            _inputRouter = inputRouter;
            _player = player;
        }

        public void FixedTick()
        {
            if (_player.Energy.Value > 0 && _player.Dead.Value == false)
            {
                var deltaTime = Time.fixedDeltaTime;
                if (_inputRouter.Accelerate)
                {
                    _player.AddForce(_player.LookDir * _settings.MoveSpeed);
                    _player.SpendEnergy(_settings.MoveCost);
                }

                if (MathF.Abs(_inputRouter.Rotation) > 0.1f)
                {
                    Rotate(_inputRouter.Rotation, deltaTime);
                    _player.SpendEnergy(_settings.RotationCost);
                }
            }

            LoopedMove();
        }

        protected override void Rotate(float direction, float deltaTime)
        {
            if (direction == 0)
                throw new InvalidOperationException(nameof(direction));

            direction = direction > 0 ? 1 : -1;
            
            _player.Rotate(direction * (_settings.RotationSpeed * deltaTime));
        }
        
        [Serializable]
        public class Settings
        {
            public float MoveSpeed;
            public float RotationSpeed;
            public float RotationCost;
            public float MoveCost;
        }
    }
}