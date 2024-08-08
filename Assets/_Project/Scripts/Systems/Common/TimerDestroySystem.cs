using Assets._Project.Scripts.Components.Common;
using Assets._Project.Scripts.Components.OneFrameComponents;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets._Project.Scripts.Systems.Common
{
    public class TimerDestroySystem : IEcsRunSystem
    {
        private readonly EcsFilter<DestroyInfoComponent, GameObjectComponent> filter = null;

        public void Run()
        {
            for (int i = 0; i < filter.GetEntitiesCount(); i++)
            {
                ref var destroyInfoComponent = ref filter.Get1(i);
                ref var gameObjectComponent = ref filter.Get2(i);

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
                    filter.GetEntity(i).Get<ActivateDestroyComponent>();
                }

                destroyInfoComponent.CurrentTime += Time.fixedDeltaTime;
            }
        }
    }
}
