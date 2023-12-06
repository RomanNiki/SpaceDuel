using _Project.Develop.Runtime.Core.Input.Components;
using _Project.Develop.Runtime.Core.Input.Systems;
using Scellecs.Morpeh.Addons.Feature;

namespace _Project.Develop.Runtime.Core.Input
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
            AddSystem(new InputAccelerateSystem());
            AddSystem(new InputRotateSystem());
            AddSystem(new InputShootSystem());
            RegisterRequest<InputAccelerateStartedEvent>();
            RegisterRequest<InputAccelerateCanceledEvent>();
            RegisterRequest<InputRotateStartedEvent>();
            RegisterRequest<InputRotateCanceledEvent>();
            RegisterRequest<InputShootStartedEvent>();
            RegisterRequest<InputShootCanceledEvent>();
        }
        
    }
}