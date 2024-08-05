using Assets._Project.Scripts.Components.Common;
using Assets._Project.Scripts.Components.OneFrameComponents;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets._Project.Scripts.Systems.Common
{
    internal class TimerDestroySystem : IEcsRunSystem
    {
        private readonly EcsFilter<DestroyInfoComponent, GameObjectComponent> filter = null;

        public void Run()
        {
            foreach (var indexEntity in filter)
            {
                ref var destroyInfoComponent = ref filter.Get1(indexEntity);
                ref var gameObjectComponent = ref filter.Get2(indexEntity);

                if (!gameObjectComponent.GameObject.activeInHierarchy)
                {
                    continue;
                }

                if (destroyInfoComponent.DestroyTime == 0)
                {
                    continue;
                }

                if (destroyInfoComponent.CurrentTime > destroyInfoComponent.DestroyTime)
                {
                    filter.GetEntity(indexEntity).Get<ActivateDestroyComponent>();
                }

                destroyInfoComponent.CurrentTime += Time.fixedDeltaTime;
            }
        }
    }
}
