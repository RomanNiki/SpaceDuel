using Core.Input;
using Engine.Input.Systems;

namespace Engine.Input
{
    public class InputFeature : InputFeatureBase
    {
        protected override void InitializeInputSystem()
        {
            AddSystem(new InputSystem(new PlayerInput()));
        }
    }
}