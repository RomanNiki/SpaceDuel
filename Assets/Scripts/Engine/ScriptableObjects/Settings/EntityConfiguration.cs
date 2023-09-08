using System;
using System.Linq;
using Engine.Converters.Base;
using Engine.Extensions;
using UnityEngine;

namespace Engine.ScriptableObjects.Settings
{
    [CreateAssetMenu(menuName = "GameEntities/EntityComponents", fileName = "Configuration")]
    public class EntityConfiguration : ScriptableObject
    {
        private static readonly TypeConverterEqualityComparer Comparer = new();

        [field: SerializeReference]
        public IConverter[] Links { get; private set; } = Array.Empty<IConverter>();
        
        protected virtual void OnValidate()
        {
            Links = Links.Distinct(Comparer).ToArray();
        }
    }
}