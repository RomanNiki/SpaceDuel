using Scellecs.Morpeh;

namespace Engine.Converters.Base
{
    public interface IConverter
    {
        void Resolve(World world, Entity entity);
    }
}