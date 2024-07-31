using Assets._Project.Scripts.Components.Common;
using Assets._Project.Scripts.Components.GamePlay;
using Assets._Project.Scripts.Components.Network;
using Assets._Project.Scripts.Components.Physics;
using ExitGames.Client.Photon.StructWrapping;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets._Project.Scripts.Systems.GamePlay
{
    internal class PlayerAirMovementSystem : IEcsRunSystem
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

                if (characterControllerComponent.CharacterController.isGrounded)
                    continue;

                var speedValue = unitMovementComponent.RunningSpeed;
                var gravityValue = unitMovementComponent.Movement.y;
                unitMovementComponent.Movement = (gameObjectComponent.GameObject.transform.forward * speedValue) + (gameObjectComponent.GameObject.transform.right * inputComponent.MovementInput.x * unitMovementComponent.RunningSpeedLeftRightOnFlying);
                unitMovementComponent.Movement.y = gravityValue;

                unitMovementComponent.Movement.y += unitMovementComponent.GravityValue * Time.deltaTime;
                characterControllerComponent.CharacterController.Move(unitMovementComponent.Movement * Time.deltaTime);
            }
        }
    }
}
