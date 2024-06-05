using Assets._Project.Scripts.Components;
using Assets._Project.Scripts.Components.Abilities;
using Assets._Project.Scripts.Components.Camera;
using Assets._Project.Scripts.Components.Object;
using Assets._Project.Scripts.Components.Player;
using Assets._Project.Scripts.Components.Unit;
using Assets._Project.Scripts.Enums;
using Assets._Project.Scripts.ScriptableObjects;
using Assets._Project.Scripts.ScriptableObjects.AbilitiesData;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets._Project.Scripts.Factories
{
    public class PlayerFactory
    {
        private readonly DiContainer diContainer;
        private readonly EcsWorld world;

        private readonly PlayerData playerData;
        private readonly CameraData cameraData;

        private readonly AccelerationAbilityData accelerationAbilityData;
        private readonly DoubleJumpAbilityData doubleJumpAbilityData;
        private readonly EnergyShieldAbilityData energyShieldAbilityData;

        private Object _playerPrefab;
        private Object _playerCameraPrefab;

        public PlayerFactory(DiContainer diContainer, EcsWorld world, PlayerData playerData, CameraData cameraData,
            AccelerationAbilityData accelerationAbilityData, DoubleJumpAbilityData doubleJumpAbilityData, EnergyShieldAbilityData energyShieldAbilityData)
        {
            this.diContainer = diContainer;
            this.world = world;
            this.playerData = playerData;
            this.cameraData = cameraData;
            this.accelerationAbilityData = accelerationAbilityData;
            this.doubleJumpAbilityData = doubleJumpAbilityData;
            this.energyShieldAbilityData = energyShieldAbilityData;
            LoadResources();
        }

        public void LoadResources()
        {
            _playerPrefab = Resources.Load("Prefabs/Player");
            _playerCameraPrefab = Resources.Load("Prefabs/PlayerCamera");
        }

        public (GameObject PlayerObject, EcsEntity PlayerEntity) CreatePlayer(Vector3 position)
        {
            var playerObject = CreatePlayerGameObject(position);
            var playerEntity = CreatePlayerEntity();
            InitializeInputComponent(in playerEntity);
            InitializeAnimationComponent(in playerEntity, playerObject);
            InitializeMovementComponent(in playerEntity, playerObject);
            InitializeRotationComponent(in playerEntity, playerObject);
            InitializeAbilityComponents(in playerEntity);
            InitializeHealthComponent(in playerEntity);
            InitializeCollisionComponent(in playerEntity, playerObject);

            var cameraGameObject = CreateCameraGameObject(position);
            var cameraEntity = CreateCameraEntity();
            InitializeCameraMovementComponent(in cameraEntity, cameraGameObject);
            InitializeFollowComponent(in cameraEntity, in playerEntity);
            InitializeCameraUIComponent(in cameraEntity, in playerEntity, cameraGameObject);

            var player = (PlayerObject: playerObject, PlayerEntity: playerEntity);
            return player;
        }

        private GameObject CreatePlayerGameObject(Vector3 position)
        {
            return diContainer.InstantiatePrefab(_playerPrefab, position, Quaternion.identity, null);
        }

        private EcsEntity CreatePlayerEntity()
        {
            return world.NewEntity();
        }

        private void InitializeInputComponent(in EcsEntity playerEntity)
        {
            ref var inputComponent = ref playerEntity.Get<InputComponent>();
        }

        private void InitializeAnimationComponent(in EcsEntity playerEntity, GameObject playerObject)
        {
            ref var unitAnimationComponent = ref playerEntity.Get<UnitAnimationComponent>();
            unitAnimationComponent.Animator = playerObject.GetComponent<Animator>();
            unitAnimationComponent.IsGrounded = "IsGrounded";
            unitAnimationComponent.IsFalling = "IsFalling";
            unitAnimationComponent.IsJump = "IsJump";
            unitAnimationComponent.InputZ = "InputZ";
            unitAnimationComponent.InputX = "InputX";
        }

        private void InitializeMovementComponent(in EcsEntity playerEntity, GameObject playerObject)
        {
            ref var unitMovementComponent = ref playerEntity.Get<UnitMovementComponent>();
            unitMovementComponent.RunningSpeed = playerData.RunningSpeed;
            unitMovementComponent.JumpHeight = playerData.JumpHeight;
            unitMovementComponent.GravityValue = playerData.GravityValue;
            unitMovementComponent.RunningSpeedLeftRightOnFlying = playerData.RunningSpeedLeftRightOnFlying;
            unitMovementComponent.Transform = playerObject.transform;
            unitMovementComponent.CharacterController = playerObject.GetComponent<CharacterController>();
        }

        private void InitializeRotationComponent(in EcsEntity playerEntity, GameObject playerObject)
        {
            ref var unitRotationComponent = ref playerEntity.Get<UnitRotationComponent>();
            unitRotationComponent.LimitRotationAngleY = playerData.LimitRotationAngleY;
            unitRotationComponent.RotationSpeedOnGround = playerData.RotationSpeedOnGround;
            unitRotationComponent.RotationSensitiveOnGround = playerData.RotationSensitiveOnGround;
            unitRotationComponent.RotationSpeedOnFlying = playerData.RotationSpeedOnFlying;
            unitRotationComponent.RotationSensitiveOnFlying = playerData.RotationSensitiveOnFlying;
            unitRotationComponent.Transform = playerObject.transform;
        }

        private void InitializeAbilityComponents(in EcsEntity playerEntity)
        {
            ref var abilityComponent = ref playerEntity.Get<AbilityComponent>();
            abilityComponent.playerData = playerData;

            abilityComponent.EnergyPointsMax = playerData.EnergyPointsMax;
            abilityComponent.EnergyPointsMin = playerData.EnergyPointsMin;
            abilityComponent.EnergyPoints = playerData.EnergyPoints;

            ref var accelerationAbilityComponent = ref playerEntity.Get<AccelerationAbilityComponent>();
            accelerationAbilityComponent.AbilityType = AbilityType.Acceleration;
            accelerationAbilityComponent.AccelerationAbilityData = accelerationAbilityData;

            ref var doubleJumpAbilityComponent = ref playerEntity.Get<DoubleJumpAbilityComponent>();
            doubleJumpAbilityComponent.AbilityType = AbilityType.DoubleJump;
            doubleJumpAbilityComponent.DoubleJumpAbilityData = doubleJumpAbilityData;

            ref var energyShieldAbilityComponent = ref playerEntity.Get<EnergyShieldAbilityComponent>();
            energyShieldAbilityComponent.AbilityType = AbilityType.EnergyShield;
            energyShieldAbilityComponent.EnergyShieldAbilityData = energyShieldAbilityData;
        }

        private void InitializeHealthComponent(in EcsEntity playerEntity)
        {
            ref var healthComponent = ref playerEntity.Get<HealthComponent>();

            healthComponent.HealthPointsMax = playerData.HealthPointsMax;
            healthComponent.HealthPointsMin = playerData.HealthPointsMin;
            healthComponent.HealthPoints = playerData.HealthPoints;
            healthComponent.DamageCoolDown = playerData.DamageCoolDown;
        }

        private void InitializeCollisionComponent(in EcsEntity playerEntity, GameObject playerObject)
        {
            ref var collisionComponent = ref playerEntity.Get<CollisionComponent>();

            collisionComponent.CollisionEntity = null;
            collisionComponent.GameObjectCollider = playerObject.GetComponent<Collider>();
        }

        public GameObject CreateCameraGameObject(Vector3 position)
        {
            return diContainer.InstantiatePrefab(_playerCameraPrefab, position, Quaternion.identity, null);
        }

        public EcsEntity CreateCameraEntity()
        {
            return world.NewEntity();
        }

        private void InitializeCameraMovementComponent(in EcsEntity cameraEntity, GameObject cameraObject)
        {
            ref var cameraMovementComponent = ref cameraEntity.Get<CameraMovementComponent>();

            cameraMovementComponent.SmoothValue = cameraData.SmoothValue;
            cameraMovementComponent.DistanceFromGround = cameraData.DistanceFromGround;
            cameraMovementComponent.Transform = cameraObject.transform;
        }

        private void InitializeFollowComponent(in EcsEntity cameraEntity, in EcsEntity playerEntity)
        {
            ref var followComponent = ref cameraEntity.Get<FollowComponent>();

            followComponent.Offset = cameraData.Offset;
            followComponent.Entity = playerEntity;
        }

        private void InitializeCameraUIComponent(in EcsEntity cameraEntity, in EcsEntity playerEntity, GameObject cameraObject)
        {
            ref var cameraUIComponent = ref cameraEntity.Get<CameraUIComponent>();

            Slider[] sliders = cameraObject.GetComponentsInChildren<Slider>();
            cameraUIComponent.EnergySlider = sliders[0]; // это костыль надо исправить
            cameraUIComponent.HealthSlider = sliders[1];

            ref var playerAbility = ref playerEntity.Get<AbilityComponent>();
            cameraUIComponent.EnergySlider.maxValue = playerAbility.EnergyPointsMax;
            cameraUIComponent.EnergySlider.minValue = playerAbility.EnergyPointsMin;
            cameraUIComponent.EnergySlider.value = playerAbility.EnergyPoints;

            ref var playerHealth = ref playerEntity.Get<HealthComponent>();
            cameraUIComponent.HealthSlider.maxValue = playerHealth.HealthPointsMax;
            cameraUIComponent.HealthSlider.minValue = playerHealth.HealthPointsMin;
            cameraUIComponent.HealthSlider.value = playerHealth.HealthPoints;
        }
    }
}
