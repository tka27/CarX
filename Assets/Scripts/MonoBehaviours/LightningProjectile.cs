using Ecs;
using Ecs.Components;
using Leopotam.EcsLite;

namespace MonoBehaviours
{
    public class LightningProjectile : Projectile
    {
        private EcsPackedEntity _selfPackedEntity;

        private void OnDisable()
        {
            if (Startup.World == null) return;
            _selfPackedEntity.Unpack(Startup.World, out var entity);
            Startup.World.DelEntity(entity);
        }

        public void Fire(EcsPackedEntity targetEntity)
        {
            var entity = Startup.World.NewEntity();
            Startup.World.GetPool<Ecs.Components.LightningProjectile>().Add(entity).Transform = transform;
            Startup.World.GetPool<HasTarget>().Add(entity).Target = targetEntity;
            _selfPackedEntity = Startup.World.PackEntity(entity);
        }
    }
}