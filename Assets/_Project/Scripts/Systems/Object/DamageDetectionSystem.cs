using Assets._Project.Scripts.Components.Object;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets._Project.Scripts.Systems.Object
{
    public class DamageDetectionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HealthComponent, CollisionComponent> filter = null;

        public void Run()
        {
            foreach (var entity in filter)
            {
                ref var healthComponent = ref filter.Get1(entity);
                ref var collisionComponent = ref filter.Get2(entity);

                if (collisionComponent.CollisionEntity == null)
                    continue;

                for (int i = 0; i < collisionComponent.CollisionEntity.Count; i++)
                {
                    if (collisionComponent.CollisionEntity[i] == EcsEntity.Null)
                        continue;

                    ref var damageComponent = ref collisionComponent.CollisionEntity[i].Get<DamageComponent>();
                    if (healthComponent.CurrentDamageCoolDown > healthComponent.DamageCoolDown)
                    {
                        healthComponent.CurrentDamageCoolDown = 0;
                        healthComponent.HealthPoints -= damageComponent.Value;
                    }
                }
                healthComponent.CurrentDamageCoolDown += Time.fixedDeltaTime;
            }
        }
    }
}
