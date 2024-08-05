using Assets._Project.Scripts.Components.Common;
using Assets._Project.Scripts.Factories;
using Leopotam.Ecs;

namespace Assets._Project.Scripts.Systems.Common
{
    public class PopulateObjectPoolSystem : IEcsInitSystem
    {
        private EcsFilter<ObjectPoolComponent> filter = null;

        private EffectsFactory effectsFactory;
        private SpawnObjectsFactory spawnObjectsFactory;

        public void Init()
        {
            foreach (var indexEntity in filter)
            {
                ref var objectPoolComponent = ref filter.Get1(indexEntity);

                for (int i = 0; i < objectPoolComponent.Size; i++)
                {
                    spawnObjectsFactory.PopulateObjectPool();
                    effectsFactory.PopulateObjectPool();
                }
            }
        }
    }
}
