namespace Modules.Container.Scripts.Interfaces
{
    public interface ILateTickable
    {
        void OnLateTick(float delta);
    }
}