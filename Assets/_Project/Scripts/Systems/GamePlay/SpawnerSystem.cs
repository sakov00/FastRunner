using Assets._Project.Scripts.Components.Common;
using Assets._Project.Scripts.Components.GamePlay;
using Assets._Project.Scripts.Components.Physics;
using Assets._Project.Scripts.UsefullScripts;
using Leopotam.Ecs;
using Unity.Entities;
using UnityEngine;
using Voody.UniLeo;

public class SpawnerSystem : IEcsRunSystem
{
    private readonly EcsFilter<TriggerComponent, SpawnerComponent, ObjectPoolComponent> filter = null;

    public void Run()
    {
        foreach (var entityIndex in filter)
        {
            ref var triggerComponent = ref filter.Get1(entityIndex);
            ref var spawnerComponent = ref filter.Get2(entityIndex);
            ref var objectPoolComponent = ref filter.Get3(entityIndex);

            if (triggerComponent.TargetEntity.IsNull())
                continue;

            if (!triggerComponent.SourceEntity.Has<SpawnerComponent>() ||
                !triggerComponent.TargetEntity.Has<PlayerComponent>())
                continue;

            ref var targetTransformComponent = ref triggerComponent.TargetEntity.Get<TransformComponent>();

            if (spawnerComponent.CurrentTime > spawnerComponent.CoolDown)
            {
                float angle = Random.Range(spawnerComponent.MinAngle, spawnerComponent.MaxAngle) * Mathf.Deg2Rad;
                float distance = Random.Range(spawnerComponent.InnerRadiusSpawn, spawnerComponent.OuterRadiusSpawn);
                float height = spawnerComponent.Height;

                float x = targetTransformComponent.transform.position.x + Mathf.Cos(angle) * distance;
                float z = targetTransformComponent.transform.position.z + Mathf.Sin(angle) * distance;
                float y = targetTransformComponent.transform.position.y + height;

                var entity = objectPoolComponent.ObjectPool.GetObject();
                if (entity == EcsEntity.Null)
                {
                    var newGameObject = (GameObject)Object.Instantiate(spawnerComponent.Prefab);
                    entity = GameObjectToEntity.AddEntity(newGameObject);

                    ref var poolableComponent = ref entity.Get<PoolableComponent>();
                    poolableComponent.ObjectPool = objectPoolComponent.ObjectPool;
                    poolableComponent.IsActive = true;
                }

                ref var destroyObjectComponent = ref entity.Get<DestroyObjectComponent>();
                destroyObjectComponent.IsActivateDestroy = true;

                entity.Get<TransformComponent>().transform.position = new Vector3(x, y, z);
                spawnerComponent.CurrentTime = 0;
            }
            else 
            {
                spawnerComponent.CurrentTime += Time.fixedDeltaTime;
            }
        }
    }
}
