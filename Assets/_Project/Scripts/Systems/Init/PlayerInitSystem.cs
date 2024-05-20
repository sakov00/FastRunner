using Assets._Project.Scripts.Components;
using Assets._Project.Scripts.Components.Abilities;
using Assets._Project.Scripts.Components.Player;
using Assets._Project.Scripts.Components.Unit;
using Assets._Project.Scripts.Enums;
using Assets._Project.Scripts.Factories;
using Assets._Project.Scripts.ScriptableObjects;
using Assets._Project.Scripts.ScriptableObjects.AbilitiesData;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace Assets._Project.Scripts.Systems.Init
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        private EcsWorld _ecsWorld = null;
        private PlayerFactory _playerFactory;
        private PlayerData _playerData;
        private CameraData _cameraData;

        private AccelerationAbilityData _accelerationAbilityData;
        private DoubleJumpAbilityData _doubleJumpAbilityData;
        private EnergyShieldAbilityData _energyShieldAbilityData;

        [Inject]
        private void Contract(PlayerFactory playerFactory, PlayerData playerData, CameraData cameraData,
            AccelerationAbilityData accelerationAbilityData, DoubleJumpAbilityData doubleJumpAbilityData, EnergyShieldAbilityData energyShieldAbilityData)
        {
            _playerFactory = playerFactory;
            _playerData = playerData;
            _cameraData = cameraData;
            _accelerationAbilityData = accelerationAbilityData;
            _doubleJumpAbilityData = doubleJumpAbilityData;
            _energyShieldAbilityData = energyShieldAbilityData;
        }

        public void Init()
        {
            var playerEntity = CreatePlayerEntity();
            CreateCameraEntity(ref playerEntity);
        }

        private EcsEntity CreatePlayerEntity()
        {
            var playerEntity = _ecsWorld.NewEntity();
            ref var inputComponent = ref playerEntity.Get<InputComponent>();
            ref var unitAnimationComponent = ref playerEntity.Get<UnitAnimationComponent>();
            ref var unitMovementComponent = ref playerEntity.Get<UnitMovementComponent>();
            ref var unitRotationComponent = ref playerEntity.Get<UnitRotationComponent>();

            ref var abilityComponent = ref playerEntity.Get<AbilityComponent>();
            ref var accelerationAbilityComponent = ref playerEntity.Get<AccelerationAbilityComponent>();
            ref var doubleJumpAbilityComponent = ref playerEntity.Get<DoubleJumpAbilityComponent>();
            ref var energyShieldAbilityComponent = ref playerEntity.Get<EnergyShieldAbilityComponent>();

            var playerObject = _playerFactory.CreatePlayer(Vector3.forward);

            unitAnimationComponent.Animator = playerObject.GetComponent<Animator>();
            unitAnimationComponent.IsGrounded = "IsGrounded";
            unitAnimationComponent.IsFalling = "IsFalling";
            unitAnimationComponent.IsJump = "IsJump";
            unitAnimationComponent.InputZ = "InputZ";
            unitAnimationComponent.InputX = "InputX";

            unitMovementComponent.RunningSpeed = _playerData.RunningSpeed;
            unitMovementComponent.JumpHeight = _playerData.JumpHeight;
            unitMovementComponent.GravityValue = _playerData.GravityValue;
            unitMovementComponent.RunningSpeedLeftRightOnFlying = _playerData.RunningSpeedLeftRightOnFlying;
            unitMovementComponent.Transform = playerObject.transform;
            unitMovementComponent.CharacterController = playerObject.GetComponent<CharacterController>();

            unitRotationComponent.LimitRotationAngleY = _playerData.LimitRotationAngleY;
            unitRotationComponent.RotationSpeedOnGround = _playerData.RotationSpeedOnGround;
            unitRotationComponent.RotationSensitiveOnGround = _playerData.RotationSensitiveOnGround;
            unitRotationComponent.RotationSpeedOnFlying = _playerData.RotationSpeedOnFlying;
            unitRotationComponent.RotationSensitiveOnFlying = _playerData.RotationSensitiveOnFlying;
            unitRotationComponent.Transform = playerObject.transform;

            abilityComponent.playerData = _playerData;
            abilityComponent.EnergyValue = _playerData.EnergyValue;

            accelerationAbilityComponent.AbilityType = AbilityType.Acceleration;
            accelerationAbilityComponent.AccelerationAbilityData = _accelerationAbilityData;

            doubleJumpAbilityComponent.AbilityType = AbilityType.DoubleJump;
            doubleJumpAbilityComponent.DoubleJumpAbilityData = _doubleJumpAbilityData;

            energyShieldAbilityComponent.AbilityType = AbilityType.EnergyShield;
            energyShieldAbilityComponent.EnergyShieldAbilityData = _energyShieldAbilityData;

            return playerEntity;
        }

        private void CreateCameraEntity(ref EcsEntity playerEntity)
        {
            var cameraEntity = _ecsWorld.NewEntity();
            ref var cameraMovementComponent = ref cameraEntity.Get<CameraMovementComponent>();
            ref var followComponent = ref cameraEntity.Get<FollowComponent>();

            var cameraObject = _playerFactory.CreatePlayerCamera(Vector3.forward);

            cameraMovementComponent.SmoothValue = _cameraData.SmoothValue;
            cameraMovementComponent.DistanceFromGround = _cameraData.DistanceFromGround;
            cameraMovementComponent.Transform = cameraObject.transform;

            followComponent.Offset = _cameraData.Offset;
            followComponent.TargetMovementComponent = playerEntity.Get<UnitMovementComponent>();
        }
    }
}
