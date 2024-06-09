using Assets._Project.Scripts.Components.Object;
using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Scripts.Systems.Object
{
    public class CollisionDetectionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CollisionComponent> collisionFilter1 = null;
        private readonly EcsFilter<CollisionComponent> collisionFilter2 = null;

        public void Run()
        {
            foreach (var firstEntity in collisionFilter1)
            {
                ref var firstCollisionComponent = ref collisionFilter1.Get1(firstEntity);

                foreach (var secondEntity in collisionFilter2)
                {
                    if (collisionFilter1.GetEntity(firstEntity) == collisionFilter2.GetEntity(secondEntity))
                    {
                        continue;
                    }

                    ref var secondCollisionComponent = ref collisionFilter2.Get1(secondEntity);

                    if (firstCollisionComponent.GameObjectCollider == null || secondCollisionComponent.GameObjectCollider == null)
                    {
                        continue;
                    }

                    if (IsColliding(firstCollisionComponent.GameObjectCollider, secondCollisionComponent.GameObjectCollider))
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
