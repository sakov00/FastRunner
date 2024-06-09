using Assets._Project.Scripts.Components.Player;
using Assets._Project.Scripts.Components.Unit;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets._Project.Scripts.Systems.Player
{
    internal class PlayerAirMovementSystem : IEcsRunSystem
    {
        private readonly EcsFilter<InputComponent, UnitMovementComponent> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var inputComponent = ref filter.Get1(i);
                ref var unitMovementComponent = ref filter.Get2(i);

                if (unitMovementComponent.CharacterController.isGrounded)
                    break;

                var speedValue = unitMovementComponent.RunningSpeed;
                var gravityValue = unitMovementComponent.Movement.y;
                unitMovementComponent.Movement = (unitMovementComponent.Transform.forward * speedValue) + (unitMovementComponent.Transform.right * inputComponent.MovementInput.x * unitMovementComponent.RunningSpeedLeftRightOnFlying);
                unitMovementComponent.Movement.y = gravityValue;

                unitMovementComponent.Movement.y += unitMovementComponent.GravityValue * Time.deltaTime;
                unitMovementComponent.CharacterController.Move(unitMovementComponent.Movement * Time.deltaTime);
            }
        }
    }
}
