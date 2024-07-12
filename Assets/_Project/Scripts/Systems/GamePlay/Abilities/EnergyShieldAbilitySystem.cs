using Assets._Project.Scripts.Components.GamePlay;
using Leopotam.Ecs;

namespace Assets._Project.Scripts.Systems.GamePlay.Abilities
{
    internal class EnergyShieldAbilitySystem : IEcsRunSystem
    {
        private readonly EcsFilter<AbilityComponent, EnergyShieldAbilityComponent> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var abilityComponent = ref filter.Get1(i);
                ref var energyShieldAbilityComponent = ref filter.Get2(i);

                if (!abilityComponent.EnergyShieldAbilityActivated)
                    return;
            }
        }
    }
}
