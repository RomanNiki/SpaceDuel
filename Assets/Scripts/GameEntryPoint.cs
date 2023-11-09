using System;
using Core.Services;
using Core.Services.Time;
using Scellecs.Morpeh;
using UnityEngine;
using VContainer;

public class GameEntryPoint : MonoBehaviour
{
    private World _world;
    private IGame _game;
    private ITimeScale _timeScale;

    [Inject]
    private void Constuct(IGame game, ITimeScale timeScale)
    {
        _game = game;
        _timeScale = timeScale;
    }

    private void Awake()
    {
        _world = World.Default;
        if (_world != null)
            _world.UpdateByUnity = false;
    }

    private void OnEnable()
    {
        _game.Start();
    }

    private void Update()
    {
        if (_game is { IsPlaying: true })
        {
            _world?.Update(Time.deltaTime * _timeScale.TimeScale);
        }
    }

    private void FixedUpdate()
    {
        if (_game is { IsPlaying: true })
        {
            _world?.FixedUpdate(Time.fixedDeltaTime * _timeScale.TimeScale);
        }
    }

    private void LateUpdate()
    {
        if (_game is { IsPlaying: true })
        {
            _world?.LateUpdate(Time.deltaTime * _timeScale.TimeScale);
        }
    }

    private void OnDestroy()
    {
        _game?.Stop();
    }
}