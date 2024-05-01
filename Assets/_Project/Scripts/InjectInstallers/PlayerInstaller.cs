using Assets._Project.Scripts.Abilities;
using Assets._Project.Scripts.Player.Controllers;
using Assets._Project.Scripts.Player.Models;
using Assets._Project.Scripts.Player.Views;
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
            BindAbilities();
        }

        private void BindPlayer()
        {
            var obj = Container.InstantiatePrefab(playerManagerSounds);
            Container.BindInstance(obj.GetComponent<PlayerManagerSounds>()).AsSingle();

            Container.BindInstance(playerData).AsSingle();
            Container.BindInstance(cameraData).AsSingle();
            Container.BindInstance(FindObjectOfType<PlayerModel>()).AsSingle();
            Container.BindInstance(FindObjectOfType<Animator>()).AsSingle();
            Container.BindInstance(FindObjectOfType<PlayerInputController>()).AsSingle();
            Container.BindInstance(FindObjectOfType<CharacterController>()).AsSingle();
            Container.BindInstance(FindObjectOfType<PlayerView>()).AsSingle();
            Container.Bind<GroundMovement>().AsSingle();
            Container.Bind<AirMovement>().AsSingle();
            Container.BindInstance(FindObjectOfType<PlayerMovementController>()).AsSingle();
        }

        private void BindAbilities()
        {
            Container.BindInstance(accelerationAbilityData).AsSingle();
            Container.BindInstance(doubleJumpAbilityData).AsSingle();
            Container.BindInstance(energyShieldAbilityData).AsSingle();

            Container.BindInterfacesAndSelfTo<AccelerationAbility>().AsSingle();
            Container.BindInterfacesAndSelfTo<DoubleJumpAbility>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnergyShieldAbility>().AsSingle();
        }
    }
}
