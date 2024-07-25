﻿using Assets._Project.Scripts.Components.Common;
using Assets._Project.Scripts.Components.GamePlay;
using Assets._Project.Scripts.Components.OneFrameComponents;
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

                spawnerComponent.IsActive = !spawnerComponent.IsActive;

                if (spawnerComponent.PointSpawn == null)
                    spawnerComponent.PointSpawn = triggerComponent.SourceEntity.Get<TransformComponent>().transform;
            }
        }
    }
}
