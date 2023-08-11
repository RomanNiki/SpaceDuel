using Core.Extensions.Pause;
using Scellecs.Morpeh;

namespace Core.Extensions
{
    public static class SystemExtensions
    {
        public static void SendMessage<T>(this World world, T component)
            where T : struct, IComponent
        {
            var pool = world.GetStash<T>();
            var entity = world.CreateEntity();
            pool.Add(entity);
            pool.Get(entity) = component;
        }

        public static IFixedSystem AddExternalPause(this IFixedSystem system, IPauseService pauseService)
        {
            return new FixedPauseProxySystem(system, pauseService);
        }

        public static ISystem AddExternalPause(this ISystem system, IPauseService pauseService)
        {
            return new PauseProxySystem(system, pauseService);
        }
    }

    public class PauseProxySystem : ISystem, IPauseHandler
    {
        private readonly ISystem _system;
        private readonly IPauseService _pauseService;
        private bool _paused;

        public PauseProxySystem(ISystem system, IPauseService pauseService)
        {
            _pauseService = pauseService;
            _system = system;
        }

        public void Dispose()
        {
            _system.Dispose();
            _pauseService.RemovePauseHandler(this);
        }

        public void OnAwake()
        {
            _pauseService.AddPauseHandler(this);
            _system.OnAwake();
        }

        public World World
        {
            get => _system.World;
            set => _system.World = value;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_paused)
            {
                return;
            }

            _system.OnUpdate(deltaTime);
        }

        public void SetPaused(bool isPaused)
        {
            _paused = isPaused;
        }
    }

    public class FixedPauseProxySystem : IFixedSystem, IPauseHandler
    {
        private readonly IFixedSystem _system;
        private readonly IPauseService _pauseService;
        private bool _paused;
        
        public FixedPauseProxySystem(IFixedSystem system, IPauseService pauseService)
        {
            _pauseService = pauseService;
            _system = system;
        }

        public void Dispose()
        {
            _system.Dispose();
            _pauseService.RemovePauseHandler(this);
        }

        public void OnAwake()
        {
            _pauseService.AddPauseHandler(this);
            _system.OnAwake();
        }

        public World World
        {
            get => _system.World;
            set => _system.World = value;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_paused)
            {
                return;
            }

            _system.OnUpdate(deltaTime);
        }

        public void SetPaused(bool isPaused)
        {
            _paused = isPaused;
        }
    }
}