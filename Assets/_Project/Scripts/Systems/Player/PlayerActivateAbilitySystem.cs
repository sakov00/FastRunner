using Assets._Project.Scripts.Components.Player;
using Assets._Project.Scripts.Enums;
using Leopotam.Ecs;

namespace Assets._Project.Scripts.Systems.Player
{
    internal class PlayerActivateAbilitySystem : IEcsRunSystem
    {
        private readonly EcsFilter<InputComponent, AbilityComponent> filter = null;

        public void Run()
        {
            foreach (var entity in filter)
            {
                ref var inputComponent = ref filter.Get1(entity);
                ref var abilityComponent = ref filter.Get2(entity);

                ProcessAbility(inputComponent.OnFirstAbility, ref abilityComponent, abilityComponent.playerData.FirstAbilityType);
                ProcessAbility(inputComponent.OnSecondAbility, ref abilityComponent, abilityComponent.playerData.SecondAbilityType);
                ProcessAbility(inputComponent.OnThirdAbility, ref abilityComponent, abilityComponent.playerData.ThirdAbilityType);
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
