using Assets._Project.Scripts.Factories;
using Assets._Project.Scripts.ScriptableObjects;
using Assets._Project.Scripts.ScriptableObjects.AbilitiesData;
using Assets._Project.Scripts.Systems.Init;
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
            BindFactories();
            BindSystems();
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

        private void BindSystems()
        {
            Container.Bind<PlayerInitSystem>().AsSingle();
        }
    }
}
