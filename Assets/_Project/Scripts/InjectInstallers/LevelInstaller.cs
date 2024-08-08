using Assets._Project.Scripts.Bootstrap;
using Assets._Project.Scripts.Factories;
using Assets._Project.Scripts.ScriptableObjects;
using Assets._Project.Scripts.UnityComponents.Handlers;
using Assets._Project.Scripts.UseFullScripts;
using UnityEngine.SceneManagement;
using Zenject;

namespace Assets._Project.Scripts.InjectInstallers
{
    public class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindConfigPoolForLevel();

            Container.Bind<PlayersFactory>().AsSingle();
            Container.Bind<SpawnObjectsFactory>().AsSingle();
            Container.Bind<EffectsFactory>().AsSingle();

            Container.BindInterfacesAndSelfTo<EcsGameStartUp>().FromComponentInHierarchy().AsSingle();

            Container.BindInterfacesAndSelfTo<CollisionHandler>().FromComponentsInHierarchy().AsTransient();
            Container.BindInterfacesAndSelfTo<TriggerHandler>().FromComponentsInHierarchy().AsTransient();

            Container.BindInterfacesAndSelfTo<PopulateObjectPools>().AsSingle();
        }

        private void BindConfigPoolForLevel()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            int sceneIndex = currentScene.buildIndex;
            if (sceneIndex != 0)
            {
                string resourcePath = $"Configs/Level{sceneIndex}PoolConfig";
                Container.Bind<ObjectPoolConfig>().FromScriptableObjectResource(resourcePath).AsSingle();
            }
        }
    }
}
