using Assets._Project.Scripts.Components;
using Assets._Project.Scripts.Components.Abilities;
using Assets._Project.Scripts.Components.Player;
using Assets._Project.Scripts.Components.Unit;
using Assets._Project.Scripts.Enums;
using Assets._Project.Scripts.ScriptableObjects;
using Assets._Project.Scripts.ScriptableObjects.AbilitiesData;
using Leopotam.Ecs;
using UnityEngine;
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

            var cameraGameObject = CreateCameraGameObject(position);
            var cameraEntity = CreateCameraEntity();
            InitializeCameraMovementComponent(in cameraEntity, cameraGameObject);
            InitializeFollowComponent(in cameraEntity, in playerEntity);

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
            abilityComponent.EnergyValue = playerData.EnergyValue;

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
            followComponent.TargetMovementComponent = playerEntity.Get<UnitMovementComponent>();
        }
    }
}
