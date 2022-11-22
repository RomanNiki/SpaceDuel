using Components.Unit.MoveComponents;
using Extensions;
using Extensions.EntityToGameObject;
using Leopotam.Ecs;
using Zenject;

namespace Presenters
{
    public class BulletPresenter : EcsUnityNotifier, IPoolable<IMemoryPool>
    {
        private View _view;

        private void Start()
        {
            _view = transform.GetProvider().Entity.Get<View>();
        }

        public void OnDespawned()
        {
            _view.ViewObject.SetPool(null);
        }

        public void OnSpawned(IMemoryPool pool)
        {
            _view.ViewObject.SetPool(pool);
        }

        public class Factory : PlaceholderFactory<BulletPresenter>
        {
        }
    }
}