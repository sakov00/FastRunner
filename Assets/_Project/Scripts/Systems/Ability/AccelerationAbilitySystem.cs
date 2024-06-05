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
            foreach (var idx in filter)
            {
                ref var unitMovement = ref filter.Get1(idx);
                ref var ability = ref filter.Get2(idx);
                ref var accelerationAbility = ref filter.Get3(idx);

                if (!ability.AccelerationAbilityActivated && !accelerationAbility.IsActive)
                    continue;

                if (ability.EnergyPoints == 0)
                {
                    DeactivateAcceleration(ref unitMovement, ref accelerationAbility);
                    continue;
                }

                if (accelerationAbility.CurrentWorkTime == 0)
                {
                    ActivateAcceleration(ref unitMovement, ref accelerationAbility);
                }

                if (accelerationAbility.CurrentWorkTime < accelerationAbility.AccelerationAbilityData.EnergyTimer)
                {
                    MaintainAcceleration(ref ability, ref accelerationAbility);
                }
                else
                {
                    DeactivateAcceleration(ref unitMovement, ref accelerationAbility);
                }
            }
        }

        private void ActivateAcceleration(ref UnitMovementComponent unitMovement, ref AccelerationAbilityComponent accelerationAbility)
        {
            unitMovement.RunningSpeed += accelerationAbility.AccelerationAbilityData.ValueSpeedUp;
            accelerationAbility.IsActive = true;
        }

        private void MaintainAcceleration(ref AbilityComponent ability, ref AccelerationAbilityComponent accelerationAbility)
        {
            accelerationAbility.CurrentWorkTime += Time.fixedDeltaTime;
            ability.EnergyPoints -= accelerationAbility.AccelerationAbilityData.EnergyPerSecond * Time.fixedDeltaTime;
        }

        private void DeactivateAcceleration(ref UnitMovementComponent unitMovement, ref AccelerationAbilityComponent accelerationAbility)
        {
            if (accelerationAbility.IsActive)
            {
                unitMovement.RunningSpeed -= accelerationAbility.AccelerationAbilityData.ValueSpeedDown;
            }
            accelerationAbility.CurrentWorkTime = 0;
            accelerationAbility.IsActive = false;
        }
    }
}
