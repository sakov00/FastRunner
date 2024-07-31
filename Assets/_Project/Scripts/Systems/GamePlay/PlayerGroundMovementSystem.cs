using Assets._Project.Scripts.Components.Common;
using Assets._Project.Scripts.Components.GamePlay;
using Assets._Project.Scripts.Components.Network;
using Assets._Project.Scripts.Components.OneFrameComponents;
using Assets._Project.Scripts.Components.Physics;
using Leopotam.Ecs;
using UnityEngine;
using static Unity.Mathematics.math;

namespace Assets._Project.Scripts.Systems.GamePlay
{
    internal class PlayerGroundMovementSystem : IEcsRunSystem
    {
        private readonly EcsFilter<InputComponent, UnitMovementComponent, CharacterControllerComponent, GameObjectComponent, PhotonViewComponent> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var inputComponent = ref filter.Get1(i);
                ref var unitMovementComponent = ref filter.Get2(i);
                ref var characterControllerComponent = ref filter.Get3(i);
                ref var gameObjectComponent = ref filter.Get4(i);
                ref var photonViewComponent = ref filter.Get5(i);

                if (!photonViewComponent.PhotonView.IsMine)
                    continue;

                if (!characterControllerComponent.CharacterController.isGrounded)
                    continue;

                if (unitMovementComponent.Movement.y < unitMovementComponent.GravityValue)
                    filter.GetEntity(i).Get<LandingComponent>();

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
