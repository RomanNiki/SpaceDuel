using System;
using UnityEngine;

namespace _Project.Develop.Runtime.Core.Extensions.Views
{
    public interface IViewObject : IDisposable
    {
        void MoveTo(Vector2 position);
        void RotateTo(float rotation);
    }
}