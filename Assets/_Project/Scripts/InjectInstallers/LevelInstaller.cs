using Assets._Project.Scripts.Factories;
using Assets._Project.Scripts.Systems.Init;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace Assets._Project.Scripts.InjectInstallers
{
    public class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindEcs();
            BindFactories();
            //BindSpawners();
            BindInitSystems();
        }

        private void BindEcs()
        {
            Container.Rebind<EcsWorld>().AsSingle();
        }

        private void BindFactories()
        {
            Container.Bind<PlayerFactory>().AsSingle();
        }

        private void BindSpawners()
        {
            Container.Bind<StoneSpawner>().AsSingle();
        }

        private void BindInitSystems()
        {
            Container.Bind<IEcsInitSystem>().To<PlayerInitSystem>().AsSingle();
            if (SystemInfo.deviceType == DeviceType.Desktop)
            {
                Container.Bind<IEcsInitSystem>().To<InputPCSystem>().AsSingle();
            }
            else if (SystemInfo.deviceType == DeviceType.Handheld)
            {
                Container.Bind<IEcsInitSystem>().To<InputMobileSystem>().AsSingle();
            }
        }
    }
}
