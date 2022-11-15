using UnityEngine;

namespace Components
{
    public interface IViewObject
    {
        Vector2 Position { get; set; }
        float Rotation { get; set; }

        void AddForce(in Vector2 vector2);
        void Destroy();
    }
}