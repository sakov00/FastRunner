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
                firstCollisionComponent.CollisionEntity = new List<EcsEntity>();

                foreach (var otherEntity in collisionFilter2)
                {
                    if (collisionFilter1.GetEntity(firstEntity) == collisionFilter2.GetEntity(otherEntity))
                    {
                        continue;
                    }

                    ref var otherCollisionComponent = ref collisionFilter2.Get1(otherEntity);
                    otherCollisionComponent.CollisionEntity = new List<EcsEntity>();

                    if (firstCollisionComponent.GameObjectCollider == null || otherCollisionComponent.GameObjectCollider == null)
                    {
                        continue;
                    }

                    if (IsColliding(firstCollisionComponent.GameObjectCollider, otherCollisionComponent.GameObjectCollider))
                    {
                        firstCollisionComponent.CollisionEntity.Add(collisionFilter2.GetEntity(otherEntity));
                        otherCollisionComponent.CollisionEntity.Add(collisionFilter1.GetEntity(firstEntity));
                    }
                }
            }
        }

        private bool IsColliding(Collider playerCollider, Collider otherCollider)
        {
            return playerCollider.bounds.Intersects(otherCollider.bounds);
        }
    }
}
