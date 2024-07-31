using Assets._Project.Scripts.Components.Common;
using Assets._Project.Scripts.Components.GamePlay;
using Assets._Project.Scripts.UsefullScripts;
using Leopotam.Ecs;
using Photon.Pun;
using UnityEngine;

namespace Assets._Project.Scripts.Systems.Common
{
    public class PopulateObjectPoolSystem : IEcsInitSystem
    {
        private EcsFilter<SpawnerComponent, ObjectPoolComponent> filter = null;

        public void Init()
        {
            foreach (var indexEntity in filter)
            {
                ref var spawnerComponent = ref filter.Get1(indexEntity);
                ref var objectPoolComponent = ref filter.Get2(indexEntity);

                for (int i = 0; i < objectPoolComponent.Size; i++)
                {
                    var newGameObject = PhotonNetwork.Instantiate(spawnerComponent.Prefab.name, Vector3.zero, Quaternion.identity);
                    var entity = GameObjectToEntity.AddEntity(newGameObject);
                    objectPoolComponent.ObjectPool.PopulatePool(entity);
                }
            }
        }
    }
}
