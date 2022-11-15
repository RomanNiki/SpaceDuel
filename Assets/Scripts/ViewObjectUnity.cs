using Components;
using JetBrains.Annotations;
using Models.Player;
using UnityEngine;

public class ViewObjectUnity : IViewObject
{
    private readonly PlayerMover.Settings _settings;
    
    public Vector2 Position
    {
        get => _rigidbody2D.position;
        set => _rigidbody2D.position = value;
    }

    public float Rotation
    {
        get => _rigidbody2D.rotation;
        set => _rigidbody2D.rotation = value;
    }

    [NotNull] private readonly Rigidbody2D _rigidbody2D;

    public ViewObjectUnity([NotNull] Rigidbody2D rigidbody2D, PlayerMover.Settings settings)
    {
        _rigidbody2D = rigidbody2D;
        _settings = settings;
    }

    public void AddForce(in Vector2 vector2)
    {
        _rigidbody2D.AddForce(vector2 * _settings.MoveSpeed);
    }

    public void Destroy()
    {
    }
}