using Assets._Project.Scripts.Bootstrap;
using Assets._Project.Scripts.Factories;
using Assets._Project.Scripts.Network;
using Assets._Project.Scripts.UnityComponents.Handlers;
using Photon.Pun;
using Zenject;

namespace Assets._Project.Scripts.InjectInstallers
{
    public class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<NetworkManager>().FromComponentsInHierarchy().AsSingle();

            Container.BindInterfacesAndSelfTo<EcsGameStartUp>().FromComponentInHierarchy().AsSingle();

            Container.BindInterfacesAndSelfTo<CollisionHandler>().FromComponentsInHierarchy().AsTransient();
            Container.BindInterfacesAndSelfTo<TriggerHandler>().FromComponentsInHierarchy().AsTransient();

            Container.Bind<PlayerFactory>().AsSingle();

        }
    }
}
