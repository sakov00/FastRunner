using Assets._Project.Scripts.Components.Abilities;
using Assets._Project.Scripts.Components.Player;
using Assets._Project.Scripts.Components.Unit;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets._Project.Scripts.Systems.Ability
{
    internal class AccelerationAbilitySystem : IEcsRunSystem
    {
        private readonly EcsFilter<UnitMovementComponent, AbilityComponent, AccelerationAbilityComponent> filter = null;
        public void Run()
        {
            foreach (var component in filter)
            {
                ref var unitMovement = ref filter.Get1(component);
                ref var ability = ref filter.Get2(component);
                ref var accelerationAbility = ref filter.Get3(component);

                if (!ability.AccelerationAbilityActivated && !accelerationAbility.IsActive)
                    return;

                if (ability.EnergyValue == 0)
                {
                    if (accelerationAbility.IsActive)
                        unitMovement.RunningSpeed -= accelerationAbility.AccelerationAbilityData.ValueSpeedDown;
                    accelerationAbility.CurrentWorkTime = 0;
                    accelerationAbility.IsActive = false;
                    return;
                }

                if (accelerationAbility.CurrentWorkTime == 0)
                {
                    unitMovement.RunningSpeed += accelerationAbility.AccelerationAbilityData.ValueSpeedUp;
                    accelerationAbility.IsActive = true;
                }
                if (accelerationAbility.CurrentWorkTime < accelerationAbility.AccelerationAbilityData.EnergyTimer)
                {
                    accelerationAbility.CurrentWorkTime += Time.fixedDeltaTime;
                    ability.EnergyValue -= accelerationAbility.AccelerationAbilityData.EnergyPerSecond * Time.fixedDeltaTime;
                }
                if (accelerationAbility.CurrentWorkTime > accelerationAbility.AccelerationAbilityData.EnergyTimer)
                {
                    unitMovement.RunningSpeed -= accelerationAbility.AccelerationAbilityData.ValueSpeedDown;
                    accelerationAbility.CurrentWorkTime = 0;
                    accelerationAbility.IsActive = false;
                }
            }
        }
    }
}
