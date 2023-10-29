using Core.Input.Components;
using Core.Input.Systems;
using Scellecs.Morpeh.Addons.Feature;

namespace Core.Input
{
    public class InputFeature : UpdateFeature
    {
        private readonly IInput _input;

        public InputFeature(IInput input)
        {
            _input = input;
        }
        
        protected override void Initialize()
        {
            AddSystem(new InputSystem(_input));
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
        
    }
}