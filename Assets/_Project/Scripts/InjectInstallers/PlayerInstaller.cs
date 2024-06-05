using Assets._Project.Scripts.Factories;
using Assets._Project.Scripts.ScriptableObjects;
using Assets._Project.Scripts.ScriptableObjects.AbilitiesData;
using Assets._Project.Scripts.Systems.Init;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace Assets._Project.Scripts.InjectInstallers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private GameObject playerManagerSounds;
        [SerializeField] private PlayerData playerData;
        [SerializeField] private CameraData cameraData;

        [SerializeField] private AccelerationAbilityData accelerationAbilityData;
        [SerializeField] private DoubleJumpAbilityData doubleJumpAbilityData;
        [SerializeField] private EnergyShieldAbilityData energyShieldAbilityData;

        public override void InstallBindings()
        {
            BindEcs();
            BindConfigs();
            BindFactories();
            BindInitSystems();
        }

        private void BindEcs()
        {
            Container.Bind<EcsWorld>().AsSingle();
        }

        private void BindConfigs()
        {
            Container.BindInstance(playerData).AsSingle();
            Container.BindInstance(cameraData).AsSingle();
            Container.BindInstance(accelerationAbilityData).AsSingle();
            Container.BindInstance(doubleJumpAbilityData).AsSingle();
            Container.BindInstance(energyShieldAbilityData).AsSingle();
        }

        private void BindFactories()
        {
            Container.Bind<PlayerFactory>().AsSingle();
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
