using _Project.Develop.Runtime.Core.Services.Factories;
using Scellecs.Morpeh;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Services.Factories.EntitiesFactories
{
    public abstract class EntityFactoryFromSo : ScriptableObject, IEntityFactory
    {
        public abstract Entity CreateEntity(in World world);
    }
}