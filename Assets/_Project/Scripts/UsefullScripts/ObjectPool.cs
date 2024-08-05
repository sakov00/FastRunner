using Assets._Project.Scripts.Components.OneFrameComponents;
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
        }

        public EcsEntity GetObject()
        {
            objectPool.TryPop(out var entity);
            return entity;
        }

        public void ReturnObject(EcsEntity entity)
        {
            objectPool.Push(entity);
            ref var activateComponent = ref entity.Get<ActivateComponent>();
            activateComponent.IsActivated = false;
        }
    }
}
