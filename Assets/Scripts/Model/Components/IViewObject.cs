using UnityEngine;

namespace Model.Components
{
    public interface IViewObject
    {
        Vector2 Position { get; set; }
        float Rotation { get; set; }
        Vector2 Velocity { get; set; }
        void MoveTo(in Vector2 vector2);
        void Destroy();
    }
}