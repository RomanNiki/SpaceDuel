using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Services.Movement.Strategies
{
    public interface IMoveStrategy
    {
        public void MoveTo(Vector3 position);
    }
}