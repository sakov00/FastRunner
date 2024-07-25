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

                if (!triggerComponent.TargetEntity.IsAlive())
                    continue;

                if (!triggerComponent.TargetEntity.Has<HealthComponent>())
                    continue;

                ref var targetHealthComponent = ref triggerComponent.TargetEntity.Get<HealthComponent>();

                if (targetHealthComponent.CurrentDamageCoolDown > targetHealthComponent.DamageCoolDown)
                {
                    targetHealthComponent.CurrentDamageCoolDown = 0;
                    targetHealthComponent.HealthPoints -= damageComponent.Value;
                }
            }
        }
    }
}
