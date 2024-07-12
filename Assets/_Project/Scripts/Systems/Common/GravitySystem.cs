using Assets._Project.Scripts.Components.Common;
using Assets._Project.Scripts.Components.GamePlay;
using Assets._Project.Scripts.Components.Physics;
using Leopotam.Ecs;
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

                if (!gravityComponent.IsGrounded)
                {
                    transformComponent.transform.position += Vector3.down * gravityComponent.GravityValue * Time.deltaTime;
                }

                if (UnityEngine.Physics.Raycast(transformComponent.transform.position, Vector3.down, gravityComponent.LengthRay))
                {
                    gravityComponent.IsGrounded = true;
                }
                else
                {
                    gravityComponent.IsGrounded = false;
                }
            }
        }
    }
}
