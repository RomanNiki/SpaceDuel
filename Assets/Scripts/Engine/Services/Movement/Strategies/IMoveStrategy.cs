using UnityEngine;

namespace Engine.Services.Movement.Strategies
{
    public interface IMoveStrategy
    {
        public void MoveTo(Vector3 position);
    }
}