using Assets._Project.Scripts.Components.Common;
using Assets._Project.Scripts.Components.GamePlay;
using Assets._Project.Scripts.Components.Physics;
using Assets._Project.Scripts.Components.Rendering;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets._Project.Scripts.Systems.GamePlay
{
    internal class PlayerAirRotationSystem : IEcsRunSystem
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

                if (characterControllerComponent.CharacterController.isGrounded)
                    break;

                var currentDegreesY = unitRotationComponent.Rotation.eulerAngles.y;
                var currentDegreesZ = 0f;
                currentDegreesY += inputComponent.MovementInput.x * unitRotationComponent.RotationSpeedOnFlying;
                currentDegreesZ -= inputComponent.MovementInput.x * inputComponent.MovementInput.z * unitRotationComponent.RotationSpeedOnFlying;

                if (currentDegreesY > 180f)
                {
                    currentDegreesY -= 360f;
                }
                float clampedRotationAngleY = Mathf.Clamp(currentDegreesY, -unitRotationComponent.LimitRotationAngleY, unitRotationComponent.LimitRotationAngleY);

                if (currentDegreesZ > 180f)
                {
                    currentDegreesZ -= 360f;
                }
                float clampedRotationAngleZ = Mathf.Clamp(currentDegreesZ, -unitRotationComponent.LimitRotationAngleY, unitRotationComponent.LimitRotationAngleY);

                RaycastHit hit;
                UnityEngine.Physics.Raycast(transformComponent.transform.position, Vector3.down, out hit);
                Quaternion surfaceRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

                unitRotationComponent.Rotation = Quaternion.Euler(surfaceRotation.eulerAngles.x, clampedRotationAngleY, clampedRotationAngleZ);
                unitRotationComponent.Rotation = Quaternion.Lerp(transformComponent.transform.rotation, unitRotationComponent.Rotation, unitRotationComponent.RotationSensitiveOnFlying * Time.deltaTime);
                transformComponent.transform.rotation = unitRotationComponent.Rotation;
            }
        }
    }
}
