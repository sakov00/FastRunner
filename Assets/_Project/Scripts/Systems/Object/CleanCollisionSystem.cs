using Assets._Project.Scripts.Components.Object;
using Leopotam.Ecs;

namespace Assets._Project.Scripts.Systems.Object
{
    internal class CleanCollisionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CollisionComponent> collisionFilter1 = null;

        public void Run()
        {
            foreach (var firstEntity in collisionFilter1)
            {
                ref var collisionComponent = ref collisionFilter1.Get1(firstEntity);
                collisionComponent.CollisionEntity.Clear();
            }
        }
    }
}
