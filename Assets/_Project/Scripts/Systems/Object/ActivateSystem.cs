using Assets._Project.Scripts.Components.Object;
using Leopotam.Ecs;

namespace Assets._Project.Scripts.Systems.Object
{
    internal class ActivateSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PooledComponent, GameObjectComponent> filter = null;

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
