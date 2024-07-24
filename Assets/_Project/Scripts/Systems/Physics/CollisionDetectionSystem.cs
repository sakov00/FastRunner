using Assets._Project.Scripts.Components.Physics;
using Leopotam.Ecs;
using System;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace Assets._Project.Scripts.Systems.Physics
{
    public class CollisionDetectionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ColliderComponent> collisionFilter = null;

        public void Run()
        {
            NativeArray<ColliderData> colliderDataArray = new NativeArray<ColliderData>(collisionFilter.GetEntitiesCount(), Allocator.TempJob);
            NativeArray<Result> collisionResults = new NativeArray<Result>(collisionFilter.GetEntitiesCount(), Allocator.TempJob);

            for (int i = 0; i < collisionFilter.GetEntitiesCount(); i++)
            {
                ref var colliderComponent = ref collisionFilter.Get1(i);
                colliderDataArray[i] = new ColliderData
                {
                    ColliderBounds = colliderComponent.Colliders[0].bounds,
                    ColliderIsTrigger = colliderComponent.Colliders[0].isTrigger,
                    IsCheckCollisions = colliderComponent.IsCheckCollisions
                };
            }

            CollisionJob collisionJob = new CollisionJob
            {
                colliders = colliderDataArray,
                results = collisionResults
            };

            JobHandle jobHandle = collisionJob.Schedule(colliderDataArray.Length, Environment.ProcessorCount * 4);
            jobHandle.Complete();

            for (int i = 0; i < collisionResults.Length; i++)
            {
                if (i == collisionResults[i].EntityIndex)
                    continue;

                if (collisionResults[i].IsTrigger)
                {
                    ref var triggerComponent = ref collisionFilter.GetEntity(collisionResults[i].EntityIndex).Get<TriggerComponent>();
                    triggerComponent.SourceEntity = collisionFilter.GetEntity(collisionResults[i].EntityIndex);
                    triggerComponent.TargetEntity = collisionFilter.GetEntity(i);
                    break;
                }
                if (collisionResults[i].IsCollision)
                {
                    ref var collisionComponent = ref collisionFilter.GetEntity(i).Get<CollisionComponent>();
                    collisionComponent.CollisionEntities.Add(collisionFilter.GetEntity(collisionResults[i].EntityIndex));
                    break;
                }
            }

            colliderDataArray.Dispose();
            collisionResults.Dispose();
        }

        struct CollisionJob : IJobParallelFor
        {
            [ReadOnly] public NativeArray<ColliderData> colliders;
            public NativeArray<Result> results;

            public void Execute(int index)
            {
                ColliderData firstColliderData = colliders[index];

                if (!firstColliderData.IsCheckCollisions)
                    return;

                for (int i = 0; i < colliders.Length; i++)
                {
                    if (i == index) 
                        continue;

                    ColliderData secondColliderData = colliders[i];

                    if (firstColliderData.ColliderBounds.Intersects(secondColliderData.ColliderBounds))
                    {
                        results[index] = new Result
                        {
                            EntityIndex = i,
                            IsTrigger = secondColliderData.ColliderIsTrigger,
                            IsCollision = !secondColliderData.ColliderIsTrigger
                        };
                        return;
                    }
                    
                }
            }
        }

        private struct ColliderData
        {
            public Bounds ColliderBounds;
            public bool ColliderIsTrigger;
            public bool IsCheckCollisions;
        }

        private struct Result
        {
            public int EntityIndex;
            public bool IsTrigger;
            public bool IsCollision;
        }
    }
}
