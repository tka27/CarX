using Ecs.Components;
using Leopotam.EcsLite;

namespace Ecs.Systems
{
    public class DamageSystem : IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var filter = Startup.World.Filter<HitableComponent>().Inc<DamageRequest>().End();
            var hitablePool = Startup.World.GetPool<HitableComponent>();
            var damageRequestPool = Startup.World.GetPool<DamageRequest>();
            foreach (var entity in filter)
            {
                ref var hitable = ref hitablePool.Get(entity);
                ref var request = ref damageRequestPool.Get(entity);
                hitable.CurrentHealth -= request.Damage;
                damageRequestPool.Del(entity);
                if (hitable.CurrentHealth > 0) continue;

                hitable.Transform.gameObject.SetActive(false);
                Startup.World.DelEntity(entity);
            }
        }
    }
}