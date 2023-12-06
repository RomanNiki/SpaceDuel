using System.Collections.Generic;
using System.Linq;
using _Project.Develop.Runtime.Engine.Providers.Systems.Base;
using Scellecs.Morpeh.Addons.Feature;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Services.Factories.SystemsFactories
{
    [CreateAssetMenu(menuName = "SpaceDuel/Factory/Systems/" + nameof(FeatureFactorySo))]
    public class FeatureFactorySo : BaseFeaturesFactorySo
    {
        [SerializeField] private BaseUpdateFeatureProvider[] _updateFeatures;
        [SerializeField] private BaseFixedUpdateFeatureProvider[] _fixedUpdateFeatures;
        [SerializeField] private BaseLateUpdateFeatureProvider[] _lateUpdateFeatures;

        public override IEnumerable<LateUpdateFeature> CreateLateUpdateFeatures(FeaturesArgs args) =>
            _lateUpdateFeatures.Select(featureProvider => featureProvider.GetFeature(args));

        public override IEnumerable<FixedUpdateFeature> CreateFixedUpdateFeatures(FeaturesArgs args) =>
            _fixedUpdateFeatures.Select(featureProvider => featureProvider.GetFeature(args));

        public override IEnumerable<UpdateFeature> CreateUpdateFeatures(FeaturesArgs args) =>
            _updateFeatures.Select(featureProvider => featureProvider.GetFeature(args));
    }
}