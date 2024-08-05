using Assets._Project.Scripts.Components.Common;
using Assets._Project.Scripts.Components.GamePlay;
using Assets._Project.Scripts.Components.OneFrameComponents;
using Assets._Project.Scripts.Components.UI;
using Leopotam.Ecs;

namespace Assets._Project.Scripts.Systems.GamePlay
{
    public class ActivateSpawner : IEcsRunSystem
    {
        private readonly EcsFilter<TriggerComponent, SpawnerComponent> filter = null;

        public void Run()
        {
            foreach (var entityIndex in filter)
            {
                ref var triggerComponent = ref filter.Get1(entityIndex);
                ref var spawnerComponent = ref filter.Get2(entityIndex);

                if (!triggerComponent.SourceEntity.IsAlive() || !triggerComponent.SourceEntity.Has<PlayerComponent>())
                    continue;

                if (triggerComponent.eventType == Enums.TriggerEventType.Enter)
                {
                    spawnerComponent.IsActivated = true;
                }
                else if (triggerComponent.eventType == Enums.TriggerEventType.Exit)
                {
                    spawnerComponent.IsActivated = false;
                }

                ref var cameraUIComponent = ref triggerComponent.SourceEntity.Get<CameraUIComponent>();
                cameraUIComponent.AttentionImage.gameObject.SetActive(spawnerComponent.IsActivated);

                if (spawnerComponent.PointSpawn == null)
                    spawnerComponent.PointSpawn = triggerComponent.SourceEntity.Get<TransformComponent>().transform;
            }
        }
    }
}
