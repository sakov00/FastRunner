using Assets._Project.Scripts.Components.GamePlay;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets._Project.Scripts.Systems.GamePlay
{
    public class HealthSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HealthComponent> filter = null;

        public void Run()
        {
            foreach (var entity in filter)
            {
                ref var healthComponent = ref filter.Get1(entity);

                healthComponent.CurrentDamageCoolDown += Time.fixedDeltaTime;
            }
        }
    }
}
