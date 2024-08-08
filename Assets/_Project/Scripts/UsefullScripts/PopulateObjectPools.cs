using Assets._Project.Scripts.Enums;
using Assets._Project.Scripts.Factories;
using Assets._Project.Scripts.Interfaces;
using Assets._Project.Scripts.ScriptableObjects;
using Zenject;

namespace Assets._Project.Scripts.UseFullScripts
{
    public class PopulateObjectPools : ICustomInitializable
    {
        [Inject] private ObjectPoolConfig objectPoolConfig;
        [Inject] private EffectsFactory effectsFactory;
        [Inject] private SpawnObjectsFactory spawnObjectsFactory;

        public void Initialize()
        {
            PopulatePool(() => spawnObjectsFactory.PopulateObjectPool(SpawnObjectType.FiredStone), objectPoolConfig.FiredStonePoolSize);

            PopulatePool(() => effectsFactory.PopulateObjectPool(SpawnEffectType.Portal), objectPoolConfig.PortalPoolSize);
            PopulatePool(() => effectsFactory.PopulateObjectPool(SpawnEffectType.ExplosionCactus), objectPoolConfig.ExplosionCactusPoolSize);
            PopulatePool(() => effectsFactory.PopulateObjectPool(SpawnEffectType.Spot), objectPoolConfig.SpotPoolSize);
        }

        private void PopulatePool(System.Action populateMethod, int poolSize)
        {
            for (int i = 0; i < poolSize; i++)
            {
                populateMethod();
            }
        }
    }
}
