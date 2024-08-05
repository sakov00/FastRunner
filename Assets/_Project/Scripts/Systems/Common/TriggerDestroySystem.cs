using Assets._Project.Scripts.Components.Common;
using Assets._Project.Scripts.Components.OneFrameComponents;
using Leopotam.Ecs;

namespace Assets._Project.Scripts.Systems.Common
{
    public class TriggerDestroySystem : IEcsRunSystem
    {
        private readonly EcsFilter<TriggerComponent, DestroyInfoComponent> filter = null;

        public void Run()
        {
            foreach (var indexEntity in filter)
            {
                ref var triggerComponent = ref filter.Get1(indexEntity);
                ref var destroyInfoComponent = ref filter.Get2(indexEntity);

                if (destroyInfoComponent.IsContactDestroyed)
                {
                    filter.GetEntity(indexEntity).Get<ActivateDestroyComponent>();
                }
            }
        }
    }
}
