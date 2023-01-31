using UnityEngine;

namespace Model.Extensions.Interfaces
{
    public interface IMoveClamper
    {
        Vector3 ClampPosition(Vector3 position);
    }
}