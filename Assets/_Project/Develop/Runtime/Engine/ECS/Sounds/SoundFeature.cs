using _Project.Develop.Runtime.Engine.ECS.Sounds.Systems;
using Scellecs.Morpeh.Addons.Feature;

namespace _Project.Develop.Runtime.Engine.ECS.Sounds
{
    public class SoundFeature : UpdateFeature
    {
        protected override void Initialize()
        {
            AddSystem(new ShotSoundSystem());
            AddSystem(new AccelerationSoundSystem());
            AddSystem(new RotateSoundSystem());
        }
    }
}