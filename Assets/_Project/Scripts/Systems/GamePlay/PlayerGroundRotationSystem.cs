using Assets._Project.Scripts.Components.Common;
using Assets._Project.Scripts.Components.GamePlay;
using Assets._Project.Scripts.Components.Physics;
using Assets._Project.Scripts.Components.Rendering;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets._Project.Scripts.Systems.GamePlay
{
    internal class PlayerGroundRotationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<InputComponent, UnitMovementComponent, CharacterControllerComponent, UnitRotationComponent, TransformComponent> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var inputComponent = ref filter.Get1(i);
                ref var unitMovementComponent = ref filter.Get2(i);
                ref var characterControllerComponent = ref filter.Get3(i);
                ref var unitRotationComponent = ref filter.Get4(i);
                ref var transformComponent = ref filter.Get5(i);

                if (!characterControllerComponent.CharacterController.isGrounded)
                    break;

                var currentDegrees = transformComponent.transform.eulerAngles.y;
                var rotationSpeed = unitRotationComponent.RotationSpeedOnGround;
                currentDegrees += inputComponent.MovementInput.x * rotationSpeed;

                if (currentDegrees > 180f)
                {
                    currentDegrees -= 360f;
                }
                float clampedRotationAngle = Mathf.Clamp(currentDegrees, -unitRotationComponent.LimitRotationAngleY, unitRotationComponent.LimitRotationAngleY);

                RaycastHit hit;
                UnityEngine.Physics.Raycast(transformComponent.transform.position, Vector3.down, out hit);
                Quaternion surfaceRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

                unitRotationComponent.Rotation = Quaternion.Euler(surfaceRotation.eulerAngles.x, clampedRotationAngle, surfaceRotation.eulerAngles.z);
                unitRotationComponent.Rotation = Quaternion.Lerp(transformComponent.transform.rotation, unitRotationComponent.Rotation, unitRotationComponent.RotationSensitiveOnGround * Time.deltaTime);
                transformComponent.transform.rotation = unitRotationComponent.Rotation;
            }
        }
    }
}
