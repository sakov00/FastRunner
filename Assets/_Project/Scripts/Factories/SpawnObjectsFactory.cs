using Assets._Project.Scripts.Components.Common;
using Assets._Project.Scripts.Components.OneFrameComponents;
using Assets._Project.Scripts.Enums;
using Assets._Project.Scripts.UsefullScripts;
using Leopotam.Ecs;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace Assets._Project.Scripts.Factories
{
    public class SpawnObjectsFactory : IFactory
    {
        private readonly ObjectPool objectPool = new ObjectPool();

        private GameObject firedStone;

        public SpawnObjectsFactory()
        {
            LoadResources();
        }

        public void LoadResources()
        {
            firedStone = (GameObject)Resources.Load("Stone_Fire");
        }

        public void PopulateObjectPool()
        {
            objectPool.PopulatePool(CreateSpawnObject(firedStone));
        }

        public EcsEntity GetSpawnObject(SpawnObjectType spawnObjectType, Vector3 position = default) 
        {
            switch (spawnObjectType)
            {
                case SpawnObjectType.FiredStone:
                    return GetSpawnObject(firedStone, position);
                default:
                    return EcsEntity.Null;
            }
        }

        private EcsEntity GetSpawnObject(GameObject spawnObject, Vector3 position = default)
        {
            var entity = objectPool.GetObject();
            if (entity != EcsEntity.Null)
            {
                ref var transformComponent = ref entity.Get<TransformComponent>();
                transformComponent.transform.position = position;
            }
            else if (entity == EcsEntity.Null)
            {
                CreateSpawnObject(spawnObject, position);
            }
            return entity;
        }

        private EcsEntity CreateSpawnObject(GameObject spawnObject, Vector3 position = default)
        {
            var newGameObject = PhotonNetwork.Instantiate(spawnObject.name, position, spawnObject.transform.rotation);
            var entity = GameObjectToEntity.AddEntity(newGameObject);

            ref var poolableComponent = ref entity.Get<PoolableComponent>();
            poolableComponent.ObjectPool = objectPool;

            ref var activateComponent = ref entity.Get<ActivateComponent>();
            activateComponent.IsActivated = false;
            
            return entity;
        }
    }
}
