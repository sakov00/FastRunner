using Assets._Project.Scripts.Components.Common;
using Leopotam.Ecs;

namespace Assets._Project.Scripts.Systems.Common
{
    public class ActivateObjectsSystem : IEcsRunSystem
    {
        private readonly EcsFilter<GameObjectComponent> filter = null;

        public void Run()
        {
            foreach (var entityIndex in filter)
            {
                ref var gameObjectComponent = ref filter.Get1(entityIndex);

                gameObjectComponent.GameObject.SetActive(gameObjectComponent.IsActive);
            }
        }
    }
}
