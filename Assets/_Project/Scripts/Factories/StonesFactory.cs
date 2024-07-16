using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Factories
{
    public class StonesFactory
    {
        private readonly ObjectPool objectPool = new ObjectPool();

        private Object firedStone;

        public StonesFactory()
        {
            LoadResources();
            CreateForObjectPool();
        }

        public void LoadResources()
        {
            firedStone = Resources.Load("Prefabs/Stone_Fire");
        }

        public void CreateForObjectPool()
        {
            //for (int i = 0; i < objectPool.poolSize; i++)
            //{
            //    var entity = CreateFiredStone(Vector3.zero);
            //    objectPool.PopulatePool(entity);
            //}
        }

        public void CreateFiredStone(Vector3 position)
        {
            var entity = objectPool.GetObject();
            if (entity == EcsEntity.Null)
            {
                var gameObject = (GameObject)Object.Instantiate(firedStone, position, Quaternion.identity);
            }
            //return entity;
        }
    }
}
