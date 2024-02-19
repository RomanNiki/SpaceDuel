using UnityEngine;

namespace _Project.Develop.Runtime.Core.Movement
{
    public interface IMoveLoopService
    {
        Vector2 LoopPosition(Vector2 position);
    }
}