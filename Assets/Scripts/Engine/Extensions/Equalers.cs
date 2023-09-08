using System.Collections.Generic;
using Engine.Converters.Base;

namespace Engine.Extensions
{
    public class TypeConverterEqualityComparer : IEqualityComparer<IConverter> {
        public bool Equals(IConverter x, IConverter y) => x != null && y != null && x.GetType() == y.GetType();

        public int GetHashCode(IConverter obj) => obj.GetType().GetHashCode();
    }
}