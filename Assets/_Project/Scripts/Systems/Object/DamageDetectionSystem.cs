using Assets._Project.Scripts.Components.Object;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets._Project.Scripts.Systems.Object
{
    public class DamageDetectionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TriggerComponent> filter = null;

        public void Run()
        {
            foreach (var entity in filter)
            {
                ref var triggerComponent = ref filter.Get1(entity);

                if (!triggerComponent.TargetEntity.HasValue)
                    continue;

                if(!triggerComponent.SourceEntity.Value.Has<DamageComponent>() ||
                   !triggerComponent.TargetEntity.Value.Has<HealthComponent>())
                    continue;

                ref var sourceDamageComponent = ref triggerComponent.SourceEntity.Value.Get<DamageComponent>();
                ref var targetHealthComponent = ref triggerComponent.TargetEntity.Value.Get<HealthComponent>();

                if (targetHealthComponent.CurrentDamageCoolDown > targetHealthComponent.DamageCoolDown)
                {
                    targetHealthComponent.CurrentDamageCoolDown = 0;
                    targetHealthComponent.HealthPoints -= sourceDamageComponent.Value;
                    triggerComponent.TargetEntity = null;

                    ref var destroyObjectComponent = ref triggerComponent.SourceEntity.Value.Get<DestroyObjectComponent>();
                    destroyObjectComponent.IsActivateDestroy = true;
                }
                else
                {
                    triggerComponent.TargetEntity = null;
                }
            }
        }
    }
}
