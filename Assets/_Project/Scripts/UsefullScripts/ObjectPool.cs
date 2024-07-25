using Assets._Project.Scripts.Components.Common;
using Leopotam.Ecs;
using System.Collections.Generic;

namespace Assets._Project.Scripts.UsefullScripts
{
    public class ObjectPool
    {
        private Stack<EcsEntity> objectPool = new Stack<EcsEntity>();

        public void PopulatePool(EcsEntity entity)
        {
            objectPool.Push(entity);
            ref var pooledComponent = ref entity.Get<PoolableComponent>();
            pooledComponent.ObjectPool = this;

            ref var gameObjectComponent = ref entity.Get<GameObjectComponent>();
            gameObjectComponent.IsActive = false;
        }

        public EcsEntity GetObject()
        {
            if (objectPool.TryPop(out var entity))
            {
                ref var gameObjectComponent = ref entity.Get<GameObjectComponent>();
                gameObjectComponent.IsActive = true;
            }
            return entity;
        }

        public void ReturnObject(EcsEntity entity)
        {
            objectPool.Push(entity);
            ref var gameObjectComponent = ref entity.Get<GameObjectComponent>();
            gameObjectComponent.IsActive = false;
        }
    }
}
