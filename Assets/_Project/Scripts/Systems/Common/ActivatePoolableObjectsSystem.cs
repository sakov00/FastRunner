using Assets._Project.Scripts.Components.Common;
using Leopotam.Ecs;

namespace Assets._Project.Scripts.Systems.Common
{
    public class ActivatePoolableObjectsSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PoolableComponent, GameObjectComponent> filter = null;

        public void Run()
        {
            foreach (var entityIndex in filter)
            {
                ref var pooledComponent = ref filter.Get1(entityIndex);
                ref var gameObjectComponent = ref filter.Get2(entityIndex);

                gameObjectComponent.GameObject.SetActive(pooledComponent.IsActive);
            }
        }
    }
}
