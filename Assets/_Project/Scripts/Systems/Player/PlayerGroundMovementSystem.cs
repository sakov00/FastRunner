using Assets._Project.Scripts.Components.Object;
using Assets._Project.Scripts.Components.Player;
using Assets._Project.Scripts.Components.Unit;
using Leopotam.Ecs;
using UnityEngine;
using static Unity.Mathematics.math;

namespace Assets._Project.Scripts.Systems.Player
{
    internal class PlayerGroundMovementSystem : IEcsRunSystem
    {
        private readonly EcsFilter<InputComponent, UnitMovementComponent, CharacterControllerComponent, GameObjectComponent> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var inputComponent = ref filter.Get1(i);
                ref var unitMovementComponent = ref filter.Get2(i);
                ref var characterControllerComponent = ref filter.Get3(i);
                ref var gameObjectComponent = ref filter.Get4(i);

                if (!characterControllerComponent.CharacterController.isGrounded)
                    break;

                var speedValue = unitMovementComponent.RunningSpeed;
                unitMovementComponent.Movement.z = gameObjectComponent.GameObject.transform.forward.z * speedValue;
                unitMovementComponent.Movement.x = gameObjectComponent.GameObject.transform.forward.x * speedValue;

                if (inputComponent.MovementInput.y != 0)
                {
                    unitMovementComponent.Movement.y = sqrt(unitMovementComponent.JumpHeight * -2f * unitMovementComponent.GravityValue);
                }
                else
                {
                    unitMovementComponent.Movement.y = unitMovementComponent.GravityValue;
                }
                characterControllerComponent.CharacterController.Move(unitMovementComponent.Movement * Time.deltaTime);
            }
        }
    }
}
