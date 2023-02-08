using Leopotam.Ecs;
using Zenject;

namespace Extensions
{
    public static class ContainerExtension
    {
        public static ConcreteIdArgConditionCopyNonLazyBinder AddRunSystem<T>(this DiContainer container)
            where T : IEcsRunSystem
        {
            var concreteIdArgConditionCopyNonLazyBinder = BindSystem<T>(container);
            concreteIdArgConditionCopyNonLazyBinder.NonLazy().BindInfo.Identifier =
                SystemsEnum.Run;
            return concreteIdArgConditionCopyNonLazyBinder;
        }

        public static ConcreteIdArgConditionCopyNonLazyBinder AddInitSystem<T>(this DiContainer container)
            where T : IEcsInitSystem
        {
            
            var concreteIdArgConditionCopyNonLazyBinder = BindSystem<T>(container);
            concreteIdArgConditionCopyNonLazyBinder.NonLazy().BindInfo.Identifier =
                SystemsEnum.Run;
            return concreteIdArgConditionCopyNonLazyBinder;
        }

        public static ConcreteIdArgConditionCopyNonLazyBinder AddFixedSystem<T>(this DiContainer container)
            where T : IEcsRunSystem
        {
            var concreteIdArgConditionCopyNonLazyBinder = BindSystem<T>(container);
            concreteIdArgConditionCopyNonLazyBinder.NonLazy().BindInfo.Identifier =
                SystemsEnum.FixedRun;
            return concreteIdArgConditionCopyNonLazyBinder;
        }

        private static ConcreteIdArgConditionCopyNonLazyBinder BindSystem<T>(this DiContainer container)
        {
            return container.BindInterfacesTo<T>().AsSingle();
        }
    }
}