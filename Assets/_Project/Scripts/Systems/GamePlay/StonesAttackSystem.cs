using Assets._Project.Scripts.Components.Common;
using Assets._Project.Scripts.Components.GamePlay;
using Assets._Project.Scripts.Components.Physics;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

public class StonesAttackSystem : IEcsRunSystem
{
    private readonly EcsFilter<TriggerComponent> filter = null;

    public void Run()
    {
        foreach (var entityIndex in filter)
        {
            ref var triggerComponent = ref filter.Get1(entityIndex);

            if (triggerComponent.TargetEntity.IsNull())
                continue;

            if (!triggerComponent.SourceEntity.Has<SpawnerComponent>() ||
                !triggerComponent.TargetEntity.Has<PlayerComponent>())
                continue;

            ref var sourceSpawnerComponent = ref triggerComponent.SourceEntity.Get<SpawnerComponent>();
            ref var targetPlayerComponent = ref triggerComponent.TargetEntity.Get<PlayerComponent>();
            ref var targetTransformComponent = ref triggerComponent.TargetEntity.Get<TransformComponent>();

            if (sourceSpawnerComponent.CurrentTime > sourceSpawnerComponent.CoolDown)
            {
                float angle = Random.Range(sourceSpawnerComponent.MinAngle, sourceSpawnerComponent.MaxAngle) * Mathf.Deg2Rad;
                float distance = Random.Range(sourceSpawnerComponent.InnerRadiusSpawn, sourceSpawnerComponent.OuterRadiusSpawn);
                float height = sourceSpawnerComponent.Height;

                float x = targetTransformComponent.transform.position.x + Mathf.Cos(angle) * distance;
                float z = targetTransformComponent.transform.position.z + Mathf.Sin(angle) * distance;
                float y = targetTransformComponent.transform.position.y + height;

                var stone = (GameObject)GameObject.Instantiate(sourceSpawnerComponent.Prefab, new Vector3(x, y, z), Quaternion.identity);
                sourceSpawnerComponent.CurrentTime = 0;
            }
            else 
            {
                sourceSpawnerComponent.CurrentTime += Time.fixedDeltaTime;
            }
        }
    }
}
