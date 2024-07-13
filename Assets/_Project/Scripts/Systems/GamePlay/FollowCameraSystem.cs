using Assets._Project.Scripts.Components.Common;
using Assets._Project.Scripts.Components.GamePlay;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets._Project.Scripts.Systems.GamePlay
{
    internal class FollowCameraSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TransformComponent, CameraMovementComponent, FollowComponent> filter = null;

        public void Run()
        {
            foreach (var component in filter)
            {
                ref var transformComponent = ref filter.Get1(component);
                ref var cameraComponent = ref filter.Get2(component);
                ref var followComponent = ref filter.Get3(component);

                var targetAngleY = followComponent.Transform.eulerAngles.y;
                var offset = followComponent.OffsetPosition;
                Vector3 rotationOffset = Quaternion.Euler(0, targetAngleY, 0) * offset;
                Vector3 newCameraPosition = followComponent.Transform.position + rotationOffset;

                transformComponent.transform.position = Vector3.Lerp(transformComponent.transform.position, newCameraPosition, cameraComponent.SmoothValue * Time.deltaTime);
                transformComponent.transform.LookAt(followComponent.Transform.position + followComponent.OffsetLookAt);
            }
        }
    }
}
