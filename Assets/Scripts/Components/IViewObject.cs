using UnityEngine;
using Zenject;

namespace Components
{
    public interface IViewObject
    {
        Vector2 Position { get; set; }
        float Rotation { get; set; }
        Vector2 Velocity { get; set; }
        void MoveTo(in Vector2 vector2);
        void SetPool(IMemoryPool pool);
        void Destroy();
    }
}