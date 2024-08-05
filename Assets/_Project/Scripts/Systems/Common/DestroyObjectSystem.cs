using Assets._Project.Scripts.Components.Common;
using Assets._Project.Scripts.Components.OneFrameComponents;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets._Project.Scripts.Systems.Common
{
    public class DestroyObjectSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ActivateDestroyComponent, DestroyInfoComponent, GameObjectComponent, TransformComponent> filter = null;

        public void Run()
        {
            foreach (var indexEntity in filter)
            {
                ref var destroyInfoComponent = ref filter.Get2(indexEntity);
                ref var gameObjectComponent = ref filter.Get3(indexEntity);
                ref var transformComponent = ref filter.Get4(indexEntity);

                if (destroyInfoComponent.Effect != null)
                {
                    var objectEffect = GameObject.Instantiate(destroyInfoComponent.Effect, transformComponent.transform.position, destroyInfoComponent.Effect.transform.rotation);

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
                }
                else
                {
                    GameObject.Destroy(gameObjectComponent.GameObject);
                    filter.GetEntity(indexEntity).Destroy();
                }

                destroyInfoComponent.CurrentTime = 0;
            }
        }
    }
}
