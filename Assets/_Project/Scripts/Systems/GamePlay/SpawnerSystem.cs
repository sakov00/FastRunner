using Assets._Project.Scripts.Components.Common;
using Assets._Project.Scripts.Components.GamePlay;
using Assets._Project.Scripts.Components.OneFrameComponents;
using Assets._Project.Scripts.Factories;
using Leopotam.Ecs;
using Unity.Entities;
using UnityEngine;

public class SpawnerSystem : IEcsRunSystem
{
    private readonly EcsFilter<SpawnerComponent> filter = null;

    private SpawnObjectsFactory firedStoneFactory;
    private EffectsFactory effectFactory;

    public void Run()
    {
        foreach (var entityIndex in filter)
        {
            ref var spawnerComponent = ref filter.Get1(entityIndex);

            if (!spawnerComponent.IsActivated)
                continue;

            if (spawnerComponent.CurrentTime < spawnerComponent.CoolDown)
            {
                spawnerComponent.CurrentTime += Time.fixedDeltaTime;
                continue;
            }
            
            float angle = Random.Range(spawnerComponent.MinAngle, spawnerComponent.MaxAngle) * Mathf.Deg2Rad;
            float distance = Random.Range(spawnerComponent.InnerRadiusSpawn, spawnerComponent.OuterRadiusSpawn);
            float height = spawnerComponent.Height;

            float x = spawnerComponent.PointSpawn.position.x + Mathf.Cos(angle) * distance;
            float z = spawnerComponent.PointSpawn.position.z + Mathf.Sin(angle) * distance;
            float y = spawnerComponent.PointSpawn.position.y + height;

            var spawnObjectEntity = firedStoneFactory.GetSpawnObject(spawnerComponent.Object, new Vector3(x, y, z));
            var spawnEffectEntity = effectFactory.GetSpawnEffect(spawnerComponent.Effect, new Vector3(x, y, z));

            spawnerComponent.CurrentTime = 0;
        }
    }
}
