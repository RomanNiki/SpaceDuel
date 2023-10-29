using Core.Services;
using Core.Services.Time;
using Scellecs.Morpeh;
using UnityEngine;
using VContainer;

public class EntryPoint : MonoBehaviour
{
    private World _world;
    private IGame _game;
    private IAssets _loadingResource;
    private ITimeScale _timeScale;

    [Inject]
    private void Constuct(IGame game, ITimeScale timeScale, IAssets loadingResource)
    {
        _game = game;
        _loadingResource = loadingResource;
        _timeScale = timeScale;
    }

    private async void Awake()
    {
        _world = World.Default;
        if (_world != null)
            _world.UpdateByUnity = false;
        await _loadingResource.Load();
    }

    private void Start()
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

    private void OnDestroy() => _game?.Stop();
}