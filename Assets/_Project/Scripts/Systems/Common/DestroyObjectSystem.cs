﻿using Assets._Project.Scripts.Components.Common;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets._Project.Scripts.Systems.Common
{
    public class DestroyObjectSystem : IEcsRunSystem
    {
        private readonly EcsFilter<DestroyObjectComponent, GameObjectComponent, TransformComponent> filter = null;

        public void Run()
        {
            foreach (var indexEntity in filter)
            {
                ref var destroyObjectComponent = ref filter.Get1(indexEntity);
                ref var gameObjectComponent = ref filter.Get2(indexEntity);
                ref var transformComponent = ref filter.Get3(indexEntity);

                if (!destroyObjectComponent.IsActivateDestroy)
                {
                    continue;
                }

                if (destroyObjectComponent.CurrentTime < destroyObjectComponent.DestroyTime)
                {
                    destroyObjectComponent.CurrentTime += Time.fixedDeltaTime;
                    continue;
                }

                if (destroyObjectComponent.Effect != null)
                {
                    var objectEffect = GameObject.Instantiate(destroyObjectComponent.Effect, transformComponent.transform.position, destroyObjectComponent.Effect.transform.rotation);

                    var particleSystem = objectEffect.GetComponent<ParticleSystem>();
                    if (particleSystem != null)
                    {
                        particleSystem.Play();
                    }
                }

                GameObject.Destroy(gameObjectComponent.GameObject);
                filter.GetEntity(indexEntity).Destroy();
            }
        }
    }
}