using Core.Extensions;
using Core.Extensions.Clear.Systems;
using Core.Input.Components;
using Core.Input.Systems;
using Cysharp.Threading.Tasks;

namespace Core.Input
{
    public abstract class InputFeatureBase : BaseMorpehFeature
    {
        protected async override UniTask InitializeSystems()
        {
            AddSystem(new DellComponentInCleanup<InputAccelerateStartedEvent>());
            AddSystem(new DellComponentInCleanup<InputAccelerateCanceledEvent>());
            AddSystem(new DellComponentInCleanup<InputRotateStartedEvent>());
            AddSystem(new DellComponentInCleanup<InputRotateCanceledEvent>());
            AddSystem(new DellComponentInCleanup<InputShootStartedEvent>());
            AddSystem(new DellComponentInCleanup<InputShootCanceledEvent>());
            
            InitializeInputSystem();
            AddSystem(new InputAccelerateSystem());
            AddSystem(new InputRotateSystem());
            AddSystem(new InputShootSystem());
        }

        protected abstract void InitializeInputSystem();
    }
}