using System.Runtime.CompilerServices;
using Cysharp.Threading.Tasks;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.Extensions.Views
{
    public static class FeatureExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async UniTask<World> AddFeatureAsync(this World world, int order, BaseMorpehFeature feature,
            bool enabled = true)
        {
            await feature.InitializeFeatureAsync(world, order, enabled);
            return world;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static World RemoveFeature(this World world, BaseMorpehFeature feature)
        {
            feature.Dispose();

            return world;
        }
    }
}