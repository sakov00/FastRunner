using Assets._Project.Scripts.Components.Common;
using Assets._Project.Scripts.Components.Physics;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets._Project.Scripts.Systems.Physics
{
    public class CollisionDetectionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ColliderComponent, TransformComponent> collisionFilter = null;

        public void Run()
        {
            for (int i = 0; i < collisionFilter.GetEntitiesCount(); i++)
            {
                ref var firstColliderComponent = ref collisionFilter.Get1(i);
                ref var firstTransformComponent = ref collisionFilter.Get2(i);

                if (!firstColliderComponent.IsCheckCollisions)
                    continue;

                for (int j = 0; j < collisionFilter.GetEntitiesCount(); j++)
                {
                    if (collisionFilter.GetEntity(i) == collisionFilter.GetEntity(j))
                    {
                        continue;
                    }

                    ref var secondColliderComponent = ref collisionFilter.Get1(j);
                    ref var secondTransformComponent = ref collisionFilter.Get2(j);

                    if (Vector3.Distance(firstTransformComponent.transform.position, secondTransformComponent.transform.position) > 20)
                    {
                        continue;
                    }

                    foreach (var firstCollider in firstColliderComponent.Colliders)
                    {
                        foreach (var secondCollider in secondColliderComponent.Colliders)
                        {
                            if (secondCollider.isTrigger)
                            {
                                if (collisionFilter.GetEntity(i).Has<TriggerComponent>())
                                    continue;

                                if (IsColliding(firstCollider, secondCollider))
                                {
                                    ref var triggerComponent = ref collisionFilter.GetEntity(j).Get<TriggerComponent>();
                                    triggerComponent.SourceEntity = collisionFilter.GetEntity(j);
                                    triggerComponent.TargetEntity = collisionFilter.GetEntity(i);
                                    break;
                                }
                            }
                            else
                            {
                                if (IsColliding(firstCollider, secondCollider))
                                {
                                    ref var collisionComponent = ref collisionFilter.GetEntity(i).Get<CollisionComponent>();
                                    collisionComponent.CollisionEntity.Add(collisionFilter.GetEntity(j));
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        private bool IsColliding(Collider firstCollider, Collider secondCollider)
        {
            return firstCollider.bounds.Intersects(secondCollider.bounds);
        }
    }
}
