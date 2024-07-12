using Assets._Project.Scripts.Components.Physics;
using Leopotam.Ecs;

namespace Assets._Project.Scripts.Systems.Physics
{
    internal class CleanCollisionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CollisionComponent> collisionFilter1 = null;

        public void Run()
        {
            foreach (var firstEntity in collisionFilter1)
            {
                ref var collisionComponent = ref collisionFilter1.Get1(firstEntity);
                if (collisionComponent.CollisionEntity == null)
                    collisionComponent.CollisionEntity = new System.Collections.Generic.List<EcsEntity>();
                else
                    collisionComponent.CollisionEntity.Clear();
            }
        }
    }
}
