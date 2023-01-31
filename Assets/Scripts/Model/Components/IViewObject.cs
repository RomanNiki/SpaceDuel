using UnityEngine;

namespace Model.Components
{
    public interface IViewObject
    {
        Vector2 Position { get; set; }
        float Rotation { get; set; }
        void MoveTo(in Vector2 vector2);
        void RotateTo(float rotation);
        void Destroy();
    }
}