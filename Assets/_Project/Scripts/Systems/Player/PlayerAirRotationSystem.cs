using Assets._Project.Scripts.Components.Player;
using Assets._Project.Scripts.Components.Unit;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets._Project.Scripts.Systems.Player
{
    internal class PlayerAirRotationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<InputComponent, UnitMovementComponent, UnitRotationComponent> filter = null;
        public void Run()
        {
            foreach (var i in filter)
            {
                ref var inputComponent = ref filter.Get1(i);
                ref var unitMovementComponent = ref filter.Get2(i);
                ref var unitRotationComponent = ref filter.Get3(i);

                if (unitMovementComponent.CharacterController.isGrounded)
                    break;

                var currentDegreesY = unitRotationComponent.Rotation.eulerAngles.y;
                var currentDegreesZ = unitRotationComponent.Rotation.eulerAngles.z;
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
                Physics.Raycast(unitRotationComponent.Transform.position, Vector3.down, out hit);
                Quaternion surfaceRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

                unitRotationComponent.Rotation = Quaternion.Euler(surfaceRotation.eulerAngles.x, clampedRotationAngleY, clampedRotationAngleZ);
                unitRotationComponent.Rotation = Quaternion.Lerp(unitRotationComponent.Transform.rotation, unitRotationComponent.Rotation, unitRotationComponent.RotationSensitiveOnFlying * Time.deltaTime);
                unitRotationComponent.Transform.rotation = unitRotationComponent.Rotation;
            }
        }
    }
}
