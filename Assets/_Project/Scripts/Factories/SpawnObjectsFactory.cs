using Assets._Project.Scripts.Components.Common;
using Assets._Project.Scripts.Components.OneFrameComponents;
using Assets._Project.Scripts.Enums;
using Assets._Project.Scripts.UseFullScripts;
using Leopotam.Ecs;
using Photon.Pun;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Zenject;

namespace Assets._Project.Scripts.Factories
{
    public class SpawnObjectsFactory : IFactory
    {
        private readonly ObjectPool firedStonePool = new ObjectPool();

        private GameObject firedStone;

        public SpawnObjectsFactory()
        {
            LoadResources();
        }

        public void LoadResources()
        {
            firedStone = (GameObject)Resources.Load("Stone_Fire");
        }

        public void PopulateObjectPool(SpawnObjectType spawnObjectType)
        {
            switch (spawnObjectType)
            {
                case SpawnObjectType.FiredStone:
                    firedStonePool.PushToPool(CreateSpawnObject(firedStone, firedStonePool));
                    break;
            }
        }

        public EcsEntity GetSpawnObject(SpawnObjectType spawnObjectType, Vector3 position = default)
        {
            switch (spawnObjectType)
            {
                case SpawnObjectType.FiredStone:
                    return GetSpawnObject(firedStone, firedStonePool, position);
                default:
                    return EcsEntity.Null;
            }
        }

        private EcsEntity GetSpawnObject(GameObject spawnObject, ObjectPool objectPool, Vector3 position = default)
        {
            var entity = objectPool.GetObject();
            if (entity != EcsEntity.Null)
            {
                ref var transformComponent = ref entity.Get<TransformComponent>();
                transformComponent.transform.position = position;
            }
            else if (entity == EcsEntity.Null)
            {
                entity = CreateSpawnObject(spawnObject, objectPool, position);
            }

            ref var activateComponent = ref entity.Get<ActivateComponent>();
            activateComponent.IsActivated = true;

            return entity;
        }

        private EcsEntity CreateSpawnObject(GameObject spawnEffect, ObjectPool objectPool, Vector3 position = default)
        {
            var newGameObject = PhotonNetwork.Instantiate(spawnEffect.name, position, spawnEffect.transform.rotation);
            var entity = GameObjectToEntity.AddEntity(newGameObject);

            ref var poolableComponent = ref entity.Get<PoolableComponent>();
            poolableComponent.ObjectPool = objectPool;

            ref var activateComponent = ref entity.Get<ActivateComponent>();
            activateComponent.IsActivated = false;

            return entity;
        }
    }
}
