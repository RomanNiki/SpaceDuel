using UnityEngine;

namespace Engine.Movement.Strategies
{
    public interface IMoveStrategy
    {
        public void MoveTo(Vector3 position);
    }
}