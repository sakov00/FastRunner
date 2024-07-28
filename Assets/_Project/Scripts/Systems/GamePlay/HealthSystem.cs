using Assets._Project.Scripts.Components.Common;
using Assets._Project.Scripts.Components.GamePlay;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets._Project.Scripts.Systems.GamePlay
{
    public class HealthSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HealthComponent, DestroyObjectComponent> filter = null;

        public void Run()
        {
            foreach (var entity in filter)
            {
                ref var healthComponent = ref filter.Get1(entity);
                ref var destroyObjectComponent = ref filter.Get2(entity);

                if (healthComponent.HealthPoints == 0)
                {
                    destroyObjectComponent.IsActivateDestroy = true;
                    destroyObjectComponent.IsTriggerDestroy = true;
                }

                healthComponent.CurrentDamageCoolDown += Time.fixedDeltaTime;
            }
        }
    }
}
