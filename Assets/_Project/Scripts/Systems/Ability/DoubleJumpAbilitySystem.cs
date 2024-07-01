using Assets._Project.Scripts.Components.Abilities;
using Assets._Project.Scripts.Components.Object;
using Assets._Project.Scripts.Components.Player;
using Assets._Project.Scripts.Components.Unit;
using Leopotam.Ecs;
using UnityEngine;
using static Unity.Mathematics.math;

namespace Assets._Project.Scripts.Systems.Ability
{
    internal class DoubleJumpAbilitySystem : IEcsRunSystem
    {
        private readonly EcsFilter<UnitMovementComponent, CharacterControllerComponent, AbilityComponent, DoubleJumpAbilityComponent> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var unitMovement = ref filter.Get1(i);
                ref var characterController = ref filter.Get2(i);
                ref var ability = ref filter.Get3(i);
                ref var doubleJumpAbility = ref filter.Get4(i);

                if (characterController.CharacterController.isGrounded)
                {
                    doubleJumpAbility.CanDoubleJump = true;
                }
                if (ability.DoubleJumpAbilityActivated && unitMovement.Movement.y < 0 && ability.EnergyPoints != 0 &&
                    !characterController.CharacterController.isGrounded && doubleJumpAbility.CanDoubleJump)
                {
                    var movement = new Vector3(0, sqrt(unitMovement.JumpHeight * -2f * unitMovement.GravityValue), 0);
                    unitMovement.Movement = movement;
                    doubleJumpAbility.CanDoubleJump = false;
                    ability.EnergyPoints -= doubleJumpAbility.EnergyCost;
                }
            }
        }
    }
}
