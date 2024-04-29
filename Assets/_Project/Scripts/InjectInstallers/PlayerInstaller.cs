using Assets._Project.Scripts.Abilities;
using Assets._Project.Scripts.ScriptableObjects;
using Assets._Project.Scripts.ScriptableObjects.AbilitiesData;
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
            BindPlayer();
            BindConfigs();
        }

        private void BindPlayer()
        {
            var obj = Container.InstantiatePrefab(playerManagerSounds);
            Container.BindInstance(obj.GetComponent<PlayerManagerSounds>()).AsSingle();
        }

        private void BindConfigs()
        {
            Container.BindInstance(playerData).AsSingle();
            Container.BindInstance(cameraData).AsSingle();

            AccelerationAbility.InjectData(accelerationAbilityData);
            DoubleJumpAbility.InjectData(doubleJumpAbilityData);
            EnergyShieldAbility.InjectData(energyShieldAbilityData);
        }
    }
}
