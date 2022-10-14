using UnityEngine;

namespace Models
{
    public abstract class Transformable
    {
        private readonly Rigidbody2D _rigidBody;

        public Transformable(Rigidbody2D rigidBody)
        {
            _rigidBody = rigidBody;
        }
        
        public Vector3 LookDir => _rigidBody.transform.forward;
        
        public float Rotation
        {
            get => _rigidBody.rotation;
            set => _rigidBody.rotation = value;
        }

        public Vector3 Position
        {
            get => _rigidBody.position;
            set => _rigidBody.position = value;
        }

        public Vector3 Velocity => _rigidBody.velocity;

        public void AddForce(Vector3 force)
        {
            _rigidBody.AddForce(force);
        }

        public void Rotate(float delta)
        {
            Rotation = Mathf.Repeat(Rotation + delta, 360f);
        }
    }
}