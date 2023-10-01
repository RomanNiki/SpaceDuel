using Core.Input.Components;
using Core.Input.Systems;
using Scellecs.Morpeh.Addons.Feature;

namespace Core.Input
{
    public abstract class InputFeatureBase : UpdateFeature
    {
        protected override void Initialize()
        {
            InitializeInputSystem();
            RegisterRequest<InputAccelerateStartedEvent>();
            RegisterRequest<InputAccelerateCanceledEvent>();
            RegisterRequest<InputRotateStartedEvent>();
            RegisterRequest<InputRotateCanceledEvent>();
            RegisterRequest<InputShootStartedEvent>();
            RegisterRequest<InputShootCanceledEvent>();
            AddSystem(new InputAccelerateSystem());
            AddSystem(new InputRotateSystem());
            AddSystem(new InputShootSystem());
        }

        protected abstract void InitializeInputSystem();
    }
}