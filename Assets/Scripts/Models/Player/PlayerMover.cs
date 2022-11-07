using System;
using UnityEngine;

namespace Models.Player
{
    public sealed class PlayerMover : Mover
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

        protected override bool CanMove()
        {
            return _player.Energy.Value > 0 && _player.Dead.Value == false;
        }

        protected override void Rotate()
        {
            if (MathF.Abs(_inputRouter.Rotation) < 0.1f)
                return;
            
            if (_inputRouter.Rotation == 0)
                throw new InvalidOperationException(nameof(_inputRouter.Rotation));

            var direction = _inputRouter.Rotation > 0 ? 1 : -1;
            
            _player.Rotate(direction * (_settings.RotationSpeed * Time.fixedDeltaTime));
            _player.SpendEnergy(_settings.RotationCost);
        }

        protected override void Move()
        {
            if (_inputRouter.Accelerate)
            {
                _player.AddForce(_player.LookDir * _settings.MoveSpeed);
                _player.SpendEnergy(_settings.MoveCost);
            }
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