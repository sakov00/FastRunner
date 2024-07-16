using Assets._Project.Scripts.Components.Common;
using Assets._Project.Scripts.Components.GamePlay;
using Assets._Project.Scripts.Components.Physics;
using Leopotam.Ecs;

namespace Assets._Project.Scripts.Systems.GamePlay
{
    public class DamageDetectionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TriggerComponent> filter = null;

        public void Run()
        {
            foreach (var entity in filter)
            {
                ref var triggerComponent = ref filter.Get1(entity);

                if (!triggerComponent.SourceEntity.IsAlive() || 
                    !triggerComponent.TargetEntity.IsAlive())
                    continue;

                if (!triggerComponent.SourceEntity.Has<DamageComponent>() ||
                    !triggerComponent.TargetEntity.Has<HealthComponent>())
                    continue;

                ref var sourceDamageComponent = ref triggerComponent.SourceEntity.Get<DamageComponent>();
                ref var targetHealthComponent = ref triggerComponent.TargetEntity.Get<HealthComponent>();

                if (targetHealthComponent.CurrentDamageCoolDown > targetHealthComponent.DamageCoolDown)
                {
                    targetHealthComponent.CurrentDamageCoolDown = 0;
                    targetHealthComponent.HealthPoints -= sourceDamageComponent.Value;
                    triggerComponent.TargetEntity = EcsEntity.Null;

                    ref var destroyObjectComponent = ref triggerComponent.SourceEntity.Get<DestroyObjectComponent>();
                    destroyObjectComponent.IsActivateDestroy = true;
                }
                else
                {
                    triggerComponent.TargetEntity = EcsEntity.Null;
                }
            }
        }
    }
}
