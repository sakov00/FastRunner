using Assets._Project.Scripts.Components.Object;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets._Project.Scripts.Systems.Object
{
    public class GravitySystem : IEcsRunSystem
    {
        private readonly EcsFilter<TransformComponent, GravityComponent> filter = null;

        public void Run()
        {
            foreach (var entity in filter)
            {
                ref var transformComponent = ref filter.Get1(entity);
                ref var gravityComponent = ref filter.Get2(entity);

                transformComponent.transform.position += Vector3.down * gravityComponent.GravityValue * Time.deltaTime;
            }
        }
    }
}
