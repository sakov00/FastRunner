using Assets._Project.Scripts.Components.Object;
using Assets._Project.Scripts.Components.Player;
using Assets._Project.Scripts.Components.Unit;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets._Project.Scripts.Systems.Player
{
    internal class PlayerGroundRotationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<InputComponent, UnitMovementComponent, CharacterControllerComponent, UnitRotationComponent> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var inputComponent = ref filter.Get1(i);
                ref var unitMovementComponent = ref filter.Get2(i);
                ref var characterControllerComponent = ref filter.Get3(i);
                ref var unitRotationComponent = ref filter.Get4(i);


                if (!characterControllerComponent.CharacterController.isGrounded)
                    break;

                var currentDegrees = unitRotationComponent.Transform.eulerAngles.y;
                var rotationSpeed = unitRotationComponent.RotationSpeedOnGround;
                currentDegrees += inputComponent.MovementInput.x * rotationSpeed;

                if (currentDegrees > 180f)
                {
                    currentDegrees -= 360f;
                }
                float clampedRotationAngle = Mathf.Clamp(currentDegrees, -unitRotationComponent.LimitRotationAngleY, unitRotationComponent.LimitRotationAngleY);

                RaycastHit hit;
                Physics.Raycast(unitRotationComponent.Transform.position, Vector3.down, out hit);
                Quaternion surfaceRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

                unitRotationComponent.Rotation = Quaternion.Euler(surfaceRotation.eulerAngles.x, clampedRotationAngle, surfaceRotation.eulerAngles.z);
                unitRotationComponent.Rotation = Quaternion.Lerp(unitRotationComponent.Transform.rotation, unitRotationComponent.Rotation, unitRotationComponent.RotationSensitiveOnGround * Time.deltaTime);
                unitRotationComponent.Transform.rotation = unitRotationComponent.Rotation;
            }
        }
    }
}
