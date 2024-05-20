using Assets._Project.Scripts.Components.Player;
using Assets._Project.Scripts.Components.Unit;
using Leopotam.Ecs;
using UnityEngine;
using static Unity.Mathematics.math;

namespace Assets._Project.Scripts.Systems.Player
{
    internal class PlayerGroundMovementSystem : IEcsRunSystem
    {
        private readonly EcsFilter<InputComponent, UnitMovementComponent> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var inputComponent = ref filter.Get1(i);
                ref var unitMovementComponent = ref filter.Get2(i);

                if (!unitMovementComponent.CharacterController.isGrounded)
                    break;

                var speedValue = unitMovementComponent.RunningSpeed;
                unitMovementComponent.Movement.z = unitMovementComponent.Transform.forward.z * speedValue;
                unitMovementComponent.Movement.x = unitMovementComponent.Transform.forward.x * speedValue;

                if (inputComponent.MovementInput.y != 0)
                {
                    unitMovementComponent.Movement.y = sqrt(unitMovementComponent.JumpHeight * -2f * unitMovementComponent.GravityValue);
                }
                else
                {
                    unitMovementComponent.Movement.y = unitMovementComponent.GravityValue;
                }
                unitMovementComponent.CharacterController.Move(unitMovementComponent.Movement * Time.deltaTime);
            }
        }
    }
}
