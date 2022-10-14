using Player.Weapon;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Models.Player
{
    public class PlayerModel : Transformable, IDamageable
    {
        private readonly MeshRenderer _renderer;
     

        public PlayerModel(float health, Rigidbody2D rigidBody,
            MeshRenderer renderer) : base(rigidBody)
        {
            Health = health;
            _renderer = renderer;
        }

        public float Health
        {
            get; private set;
        }
        
        public MeshRenderer Renderer => _renderer;

        public bool IsDead
        {
            get; set;
        }

        public void TakeDamage(float healthLoss)
        {
            Health = Mathf.Max(0.0f, Health - healthLoss);
        }
        
  
    }
}