using Assets._Project.Scripts.Components.GamePlay;
using Assets._Project.Scripts.Components.OneFrameComponents;
using Leopotam.Ecs;

namespace Assets._Project.Scripts.Systems.GamePlay
{
    public class TriggerDamageDetectionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TriggerComponent, DamageComponent> filter = null;

        public void Run()
        {
            foreach (var entity in filter)
            {
                ref var triggerComponent = ref filter.Get1(entity);
                ref var damageComponent = ref filter.Get2(entity);

                if (!triggerComponent.SourceEntity.IsAlive())
                    continue;

                if (!triggerComponent.SourceEntity.Has<HealthComponent>())
                    continue;

                ref var targetHealthComponent = ref triggerComponent.SourceEntity.Get<HealthComponent>();

                if (targetHealthComponent.CurrentDamageCoolDown > targetHealthComponent.DamageCoolDown)
                {
                    targetHealthComponent.CurrentDamageCoolDown = 0;
                    targetHealthComponent.HealthPoints -= damageComponent.HealthValue;

                    if (triggerComponent.SourceEntity.Has<AbilityComponent>())
                    {
                        ref var abilityComponent = ref triggerComponent.SourceEntity.Get<AbilityComponent>();
                        abilityComponent.EnergyPoints -= damageComponent.EnergyValue;
                    }
                }
            }
        }
    }
}
