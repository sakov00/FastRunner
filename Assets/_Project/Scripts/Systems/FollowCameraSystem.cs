using Assets._Project.Scripts.Components;
using Assets._Project.Scripts.Components.Camera;
using Assets._Project.Scripts.Components.Object;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets._Project.Scripts.Systems
{
    internal class FollowCameraSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CameraMovementComponent, FollowComponent> filter = null;

        public void Run()
        {
            foreach (var component in filter)
            {
                ref var cameraComponent = ref filter.Get1(component);
                ref var followComponent = ref filter.Get2(component);

                ref var targetGameObjectComponent = ref followComponent.Entity.Get<GameObjectComponent>();
                ref var characterControllerComponent = ref followComponent.Entity.Get<CharacterControllerComponent>();

                var targetAngleY = targetGameObjectComponent.GameObject.transform.eulerAngles.y;
                var offset = followComponent.Offset;
                Vector3 rotationOffset = Quaternion.Euler(0, targetAngleY, 0) * offset;
                Vector3 newCameraPosition = targetGameObjectComponent.GameObject.transform.position + rotationOffset;

                Ray ray = new Ray(newCameraPosition, Vector3.down);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit) && characterControllerComponent.CharacterController.isGrounded)
                {
                    newCameraPosition.y = hit.point.y + cameraComponent.DistanceFromGround;
                }

                cameraComponent.Transform.position = Vector3.Lerp(cameraComponent.Transform.position, newCameraPosition, cameraComponent.SmoothValue * Time.deltaTime);
                cameraComponent.Transform.LookAt(targetGameObjectComponent.GameObject.transform);
            }
        }
    }
}
