using Assets._Project.Scripts.Components.GamePlay;
using Assets._Project.Scripts.Components.OneFrameComponents;
using Leopotam.Ecs;

namespace Assets._Project.Scripts.Systems.GamePlay
{
    internal class CollisionDamageDetectionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CollisionComponent, HealthComponent> filter = null;

        public void Run()
        {
            foreach (var entityIndex in filter)
            {
                ref var collisionComponent = ref filter.Get1(entityIndex);
                ref var healthComponent = ref filter.Get2(entityIndex);

                if (!collisionComponent.TargetEntity.Has<DamageComponent>())
                    continue;

                ref var damageComponent = ref collisionComponent.TargetEntity.Get<DamageComponent>();
                if (healthComponent.CurrentDamageCoolDown > healthComponent.DamageCoolDown)
                {
                    healthComponent.CurrentDamageCoolDown = 0;
                    healthComponent.HealthPoints -= damageComponent.HealthValue;

                    if (collisionComponent.SourceEntity.Has<AbilityComponent>())
                    {
                        ref var abilityComponent = ref collisionComponent.SourceEntity.Get<AbilityComponent>();
                        abilityComponent.EnergyPoints -= damageComponent.EnergyValue;
                    }
                }
            }
        }
    }
}
