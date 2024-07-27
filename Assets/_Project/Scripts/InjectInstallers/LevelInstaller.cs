using Assets._Project.Scripts.Bootstrap;
using Assets._Project.Scripts.UnityComponents.Handlers;
using Zenject;

namespace Assets._Project.Scripts.InjectInstallers
{
    public class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EcsGameStartUp>().FromComponentInHierarchy().AsSingle();

            Container.BindInterfacesAndSelfTo<CollisionHandler>().FromComponentsInHierarchy().AsTransient();
            Container.BindInterfacesAndSelfTo<TriggerHandler>().FromComponentsInHierarchy().AsTransient();
        }
    }
}
