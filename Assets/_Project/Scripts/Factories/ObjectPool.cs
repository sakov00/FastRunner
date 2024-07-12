using Assets._Project.Scripts.Components.GamePlay;
using Leopotam.Ecs;
using System.Collections.Generic;

namespace Assets._Project.Scripts.Factories
{
    public class ObjectPool
    {
        private Stack<EcsEntity> objectPool;
        private int poolSize;

        public ObjectPool(int initialSize = 10)
        {
            poolSize = initialSize;
        }

        public void PopulatePool(EcsEntity entity)
        {
            objectPool.Push(entity);
            ref var pooledComponent = ref entity.Get<PooledComponent>();
            pooledComponent.ObjectPool = this;
            pooledComponent.IsActive = false;
        }

        public EcsEntity GetObject()
        {
            var entity = objectPool.Pop();
            ref var pooledComponent = ref entity.Get<PooledComponent>();
            pooledComponent.IsActive = true;
            return entity != null ? entity : EcsEntity.Null;
        }

        public void ReturnObject(EcsEntity entity)
        {
            if (poolSize < objectPool.Count)
            {
                objectPool.Push(entity);
                ref var pooledComponent = ref entity.Get<PooledComponent>();
                pooledComponent.IsActive = false;
            }
        }
    }
}
