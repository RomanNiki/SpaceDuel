using Engine.Sounds.Systems;
using Scellecs.Morpeh.Addons.Feature;

namespace Engine.Sounds
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