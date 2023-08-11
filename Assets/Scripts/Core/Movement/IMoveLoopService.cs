using UnityEngine;

namespace Core.Movement
{
    public interface IMoveLoopService
    {
        Vector2 LoopPosition(Vector2 position);
    }
}