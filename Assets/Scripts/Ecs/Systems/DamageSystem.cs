using Ecs.Components;
using Leopotam.EcsLite;

namespace Ecs.Systems
{
    public class DamageSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter = Startup.World.Filter<Hitable>().Inc<DamageRequest>().End();

        private readonly EcsPool<Hitable> _hitablePool = Startup.World.GetPool<Hitable>();
        private readonly EcsPool<DamageRequest> _damageRequestPool = Startup.World.GetPool<DamageRequest>();

        public void Run(EcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var hitable = ref _hitablePool.Get(entity);
                ref var request = ref _damageRequestPool.Get(entity);
                hitable.CurrentHealth -= request.Damage;
                _damageRequestPool.Del(entity);
                if (hitable.CurrentHealth > 0) continue;

                hitable.Transform.gameObject.SetActive(false);
                Startup.World.DelEntity(entity);
            }
        }
    }
}