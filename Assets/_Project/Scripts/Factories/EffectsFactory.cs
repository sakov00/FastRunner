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
    public class EffectsFactory : IFactory
    {
        private readonly ObjectPool objectPool = new ObjectPool();

        private GameObject portal;
        private GameObject explosionCactus;
        private GameObject spot;

        public EffectsFactory()
        {
            LoadResources();
        }

        public void LoadResources()
        {
            portal = (GameObject)Resources.Load("PortalStone");
            explosionCactus = (GameObject)Resources.Load("ExplosionCactus");
            spot = (GameObject)Resources.Load("Spot");
        }

        public void PopulateObjectPool()
        {
            objectPool.PopulatePool(CreateSpawnEffect(portal));
        }

        public EcsEntity GetSpawnEffect(SpawnEffectType spawnEffectType, Vector3 position = default)
        {
            switch (spawnEffectType)
            {
                case SpawnEffectType.Portal:
                    return GetSpawnEffect(portal, position);
                case SpawnEffectType.ExplosionCactus:
                    return GetSpawnEffect(explosionCactus,position);
                case SpawnEffectType.Spot:
                    return GetSpawnEffect(spot, position);
                default:
                    return EcsEntity.Null;
            }
        }

        private EcsEntity GetSpawnEffect(GameObject spawnObject, Vector3 position = default)
        {
            var entity = objectPool.GetObject();
            if (entity != EcsEntity.Null)
            {
                ref var transformComponent = ref entity.Get<TransformComponent>();
                transformComponent.transform.position = position;
            }
            else if (entity == EcsEntity.Null)
            {
                CreateSpawnEffect(spawnObject, position);
            }
            return entity;
        }

        private EcsEntity CreateSpawnEffect(GameObject spawnEffect, Vector3 position = default)
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
