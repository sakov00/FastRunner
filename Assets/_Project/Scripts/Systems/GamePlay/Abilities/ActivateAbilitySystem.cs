using Assets._Project.Scripts.Components.GamePlay;
using Assets._Project.Scripts.Enums;
using Leopotam.Ecs;

namespace Assets._Project.Scripts.Systems.GamePlay.Abilities
{
    internal class ActivateAbilitySystem : IEcsRunSystem
    {
        private readonly EcsWorld world = null;
        private readonly EcsFilter<InputComponent, AbilityComponent> filter = null;

        public void Run()
        {
            foreach (var entityIndex in filter)
            {
                ref var inputComponent = ref filter.Get1(entityIndex);
                ref var abilityComponent = ref filter.Get2(entityIndex);

                ProcessAbility(inputComponent.OnFirstAbility, ref abilityComponent, abilityComponent.FirstAbilityType);
                ProcessAbility(inputComponent.OnSecondAbility, ref abilityComponent, abilityComponent.SecondAbilityType);
                ProcessAbility(inputComponent.OnThirdAbility, ref abilityComponent, abilityComponent.ThirdAbilityType);
            }
        }

        private void ProcessAbility(bool isAbilityActivated, ref AbilityComponent abilityComponent, AbilityType abilityType)
        {
            switch (abilityType)
            {
                case AbilityType.Acceleration:
                    abilityComponent.AccelerationAbilityActivated = isAbilityActivated;
                    break;
                case AbilityType.DoubleJump:
                    abilityComponent.DoubleJumpAbilityActivated = isAbilityActivated;
                    break;
                case AbilityType.EnergyShield:
                    abilityComponent.EnergyShieldAbilityActivated = isAbilityActivated;
                    break;
                default:
                    break;
            }
        }
    }
}
