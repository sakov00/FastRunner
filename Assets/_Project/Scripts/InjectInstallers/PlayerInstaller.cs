using Assets._Project.InputSystem;
using Assets._Project.Scripts.Factories;
using Assets._Project.Scripts.ScriptableObjects;
using Assets._Project.Scripts.ScriptableObjects.AbilitiesData;
using Assets._Project.Scripts.Spawners;
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
            BindConfigs();
            BindPlayerInputs();
            BindFactories();
            BindSpawners();
        }

        private void BindConfigs()
        {
            Container.BindInstance(playerData).AsSingle();
            Container.BindInstance(cameraData).AsSingle();
            Container.BindInstance(accelerationAbilityData).AsSingle();
            Container.BindInstance(doubleJumpAbilityData).AsSingle();
            Container.BindInstance(energyShieldAbilityData).AsSingle();
        }

        private void BindPlayerInputs()
        {
            if (SystemInfo.deviceType == DeviceType.Desktop)
            {
                Container.BindInterfacesAndSelfTo<PlayerInputPCController>().AsSingle();
            }
            else if (SystemInfo.deviceType == DeviceType.Handheld)
            {
                Container.BindInterfacesAndSelfTo<PlayerInputMobileController>().AsSingle();
            }
        }

        private void BindFactories()
        {
            Container.Bind<PlayerFactory>().AsSingle();
        }

        private void BindSpawners()
        {
            Container.BindInstance(FindObjectOfType<PlayerSpawner>()).AsSingle();
        }
    }
}
