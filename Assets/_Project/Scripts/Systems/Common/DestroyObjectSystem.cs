using Assets._Project.Scripts.Components.Common;
using Assets._Project.Scripts.Components.OneFrameComponents;
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

                if (!gameObjectComponent.IsActive)
                    continue;

                if (!destroyObjectComponent.IsActivateDestroy &&
                    !(destroyObjectComponent.IsTriggerDestroy && filter.GetEntity(indexEntity).Has<TriggerComponent>()))
                    continue;

                if (destroyObjectComponent.CurrentTime < destroyObjectComponent.DestroyTime)
                {
                    destroyObjectComponent.CurrentTime += Time.deltaTime;
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

                if (filter.GetEntity(indexEntity).Has<PoolableComponent>())
                {
                    var entity = filter.GetEntity(indexEntity);
                    ref var pooledComponent = ref entity.Get<PoolableComponent>();
                    pooledComponent.ObjectPool.ReturnObject(entity);
                    destroyObjectComponent.IsActivateDestroy = false;
                }
                else
                {
                    GameObject.Destroy(gameObjectComponent.GameObject);
                    filter.GetEntity(indexEntity).Destroy();
                }
                destroyObjectComponent.CurrentTime = 0;
            }
        }
    }
}
