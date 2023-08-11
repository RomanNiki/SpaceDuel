using Core.Extensions.Factories;
using Scellecs.Morpeh;
using UnityEngine;

namespace Factories.EntitiesFactories
{
    public abstract class EntityFactoryFromSo : ScriptableObject, IEntityFactory
    {
        public abstract Entity CreateEntity(in World world);
        
    }
}