using Assets._Project.Scripts.Components.Common;
using Assets._Project.Scripts.Components.GamePlay;
using Assets._Project.Scripts.UsefullScripts;
using Leopotam.Ecs;
using UnityEngine;

public class SpawnerSystem : IEcsRunSystem
{
    private readonly EcsFilter<SpawnerComponent, ObjectPoolComponent> filter = null;

    public void Run()
    {
        foreach (var entityIndex in filter)
        {
            ref var spawnerComponent = ref filter.Get1(entityIndex);
            ref var objectPoolComponent = ref filter.Get2(entityIndex);

            if (!spawnerComponent.IsActive)
                continue;

            if (spawnerComponent.CurrentTime > spawnerComponent.CoolDown)
            {
                float angle = Random.Range(spawnerComponent.MinAngle, spawnerComponent.MaxAngle) * Mathf.Deg2Rad;
                float distance = Random.Range(spawnerComponent.InnerRadiusSpawn, spawnerComponent.OuterRadiusSpawn);
                float height = spawnerComponent.Height;

                float x = spawnerComponent.PointSpawn.position.x + Mathf.Cos(angle) * distance;
                float z = spawnerComponent.PointSpawn.position.z + Mathf.Sin(angle) * distance;
                float y = spawnerComponent.PointSpawn.position.y + height;

                var entity = objectPoolComponent.ObjectPool.GetObject();
                if (entity == EcsEntity.Null)
                {
                    var newGameObject = (GameObject)Object.Instantiate(spawnerComponent.Prefab);
                    entity = GameObjectToEntity.AddEntity(newGameObject);

                    ref var poolableComponent = ref entity.Get<PoolableComponent>();
                    poolableComponent.ObjectPool = objectPoolComponent.ObjectPool;

                    ref var gameObjectComponent = ref entity.Get<GameObjectComponent>();
                    gameObjectComponent.IsActive = true;
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
