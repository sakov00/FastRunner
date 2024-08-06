using Assets._Project.Scripts.Components.Common;
using Assets._Project.Scripts.Components.OneFrameComponents;
using Assets._Project.Scripts.Components.Rendering;
using Assets._Project.Scripts.Enums;
using Assets._Project.Scripts.Factories;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets._Project.Scripts.Systems.Common
{
    public class DestroyObjectSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ActivateDestroyComponent, DestroyInfoComponent, GameObjectComponent, TransformComponent> filter = null;

        private EffectsFactory effectFactory;

        public void Run()
        {
            foreach (var indexEntity in filter)
            {
                ref var destroyInfoComponent = ref filter.Get2(indexEntity);
                ref var gameObjectComponent = ref filter.Get3(indexEntity);
                ref var transformComponent = ref filter.Get4(indexEntity);

                if (destroyInfoComponent.Effect != SpawnEffectType.None)
                {
                    effectFactory.GetSpawnEffect(destroyInfoComponent.Effect, transformComponent.transform.position);
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
