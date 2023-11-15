using _Project.Develop.Runtime.Core.Extensions.Views;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Views
{
    public abstract class ViewObjectBase : IViewObject
    {
        public abstract void MoveTo(Vector2 position);

        public abstract void RotateTo(float rotation);

        public abstract void Dispose();
    }
}