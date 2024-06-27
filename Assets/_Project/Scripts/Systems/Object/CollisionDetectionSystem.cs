using Assets._Project.Scripts.Components.Object;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets._Project.Scripts.Systems.Object
{
    public class CollisionDetectionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CollisionComponent, ColliderComponent> collisionFilter1 = null;
        private readonly EcsFilter<CollisionComponent, ColliderComponent> collisionFilter2 = null;

        public void Run()
        {
            foreach (var firstEntity in collisionFilter1)
            {
                ref var firstCollisionComponent = ref collisionFilter1.Get1(firstEntity);
                ref var firstColliderComponent = ref collisionFilter1.Get2(firstEntity);

                foreach (var secondEntity in collisionFilter2)
                {
                    if (collisionFilter1.GetEntity(firstEntity) == collisionFilter2.GetEntity(secondEntity))
                    {
                        continue;
                    }

                    ref var secondCollisionComponent = ref collisionFilter2.Get1(secondEntity);
                    ref var secondColliderComponent = ref collisionFilter2.Get2(secondEntity);

                    if (firstColliderComponent.Collider == null || secondColliderComponent.Collider == null)
                    {
                        continue;
                    }

                    if (IsColliding(firstColliderComponent.Collider, secondColliderComponent.Collider))
                    {
                        firstCollisionComponent.CollisionEntity.Add(collisionFilter2.GetEntity(secondEntity));
                        secondCollisionComponent.CollisionEntity.Add(collisionFilter1.GetEntity(firstEntity));
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
