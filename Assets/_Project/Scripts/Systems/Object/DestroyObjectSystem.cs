using Assets._Project.Scripts.Components.Object;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets._Project.Scripts.Systems.Object
{
    public class DestroyObjectSystem : IEcsRunSystem
    {
        private readonly EcsFilter<DestroyObjectComponent, GameObjectComponent, TransformComponent> filter = null;

        public void Run()
        {
            foreach (var indexEntity in filter)
            {
                ref var destroyObjectComponent = ref filter.Get1(indexEntity);
                ref var gameObjectComponent = ref filter.Get2(indexEntity);
                ref var transformComponent = ref filter.Get3(indexEntity);

                if (!destroyObjectComponent.IsActivateDestroy)
                    continue;

                GameObject.Destroy(gameObjectComponent.GameObject);

                if (destroyObjectComponent.ParticleSystem != null)
                {
                    destroyObjectComponent.ParticleSystem.transform.position = transformComponent.transform.position + new Vector3(0, 2, 0);
                    destroyObjectComponent.ParticleSystem.Play();
                }

                filter.GetEntity(indexEntity).Destroy();
            }
        }
    }
}
