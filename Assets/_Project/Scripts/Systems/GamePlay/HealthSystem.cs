using Assets._Project.Scripts.Components.Common;
using Assets._Project.Scripts.Components.GamePlay;
using Assets._Project.Scripts.Components.OneFrameComponents;
using ExitGames.Client.Photon.StructWrapping;
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

                if (healthComponent.HealthPoints == 0)
                {
                    filter.GetEntity(entity).Get<ActivateDestroyComponent>();
                }

                healthComponent.CurrentDamageCoolDown += Time.fixedDeltaTime;
            }
        }
    }
}
