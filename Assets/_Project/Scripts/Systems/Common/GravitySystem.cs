using Assets._Project.Scripts.Components.Common;
using Assets._Project.Scripts.Components.GamePlay;
using Assets._Project.Scripts.Components.Physics;
using Leopotam.Ecs;
using System.Linq;
using UnityEngine;

namespace Assets._Project.Scripts.Systems.Common
{
    public class GravitySystem : IEcsRunSystem
    {
        private readonly EcsFilter<TransformComponent, ColliderComponent, GravityComponent> filter = null;

        public void Run()
        {
            foreach (var entity in filter)
            {
                ref var transformComponent = ref filter.Get1(entity);
                ref var colliderComponent = ref filter.Get2(entity);
                ref var gravityComponent = ref filter.Get3(entity);

                var bounds = colliderComponent.Colliders.First().bounds;
                Vector3 bottomPoint = bounds.center - Vector3.up * bounds.extents.y;
                float distanceToBottom = Vector3.Distance(bounds.center, bottomPoint);

                if (!UnityEngine.Physics.Raycast(bounds.center, Vector3.down, distanceToBottom))
                {
                    transformComponent.transform.position += Vector3.down * gravityComponent.GravityValue * Time.deltaTime;
                    gravityComponent.IsGrounded = false;
                }
                else
                {
                    gravityComponent.IsGrounded = true;
                }
            }
        }
    }
}
