using UnityEngine;

namespace Views.Moveable
{
    public interface IMoveStrategy
    {
        public void MoveTo(Vector2 position);
    }
}