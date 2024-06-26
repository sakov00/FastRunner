﻿using Assets._Project.Scripts.Spawners;
using Assets._Project.Scripts.Systems;
using Assets._Project.Scripts.Systems.Ability;
using Assets._Project.Scripts.Systems.Init;
using Assets._Project.Scripts.Systems.Object;
using Assets._Project.Scripts.Systems.Player;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Bootstrap
{
    public class EcsGameStartUp : MonoBehaviour
    {
        private EcsWorld world;

        private EcsSystems initUpdateSystems;
        private EcsSystems fixedUpdateSystems;
        private EcsSystems updateSystems;
        private EcsSystems lateUpdateSystems;


        private void Start()
        {
            world = new EcsWorld();
            WorldHandler.Init(world);

            DeclareInitSystems();
            DeclareFixedUpdateSystems();
            DeclareUpdateSystems();
            DeclareLateUpdateSystems();
        }

        private void DeclareInitSystems()
        {
            initUpdateSystems = new EcsSystems(world);

            if (SystemInfo.deviceType == DeviceType.Desktop)
            {
                initUpdateSystems.Add(new InputPCSystem());
            }
            else if (SystemInfo.deviceType == DeviceType.Handheld)
            {
                initUpdateSystems.Add(new InputMobileSystem());
            }

            initUpdateSystems.ConvertScene();
            initUpdateSystems.Init();
        }

        private void DeclareFixedUpdateSystems()
        {
            fixedUpdateSystems = new EcsSystems(world);

            fixedUpdateSystems.Add(new PlayerActivateAbilitySystem());
            fixedUpdateSystems.Add(new AccelerationAbilitySystem());
            fixedUpdateSystems.Add(new DoubleJumpAbilitySystem());
            fixedUpdateSystems.Add(new EnergyShieldAbilitySystem());

            fixedUpdateSystems.Add(new CleanCollisionSystem());
            fixedUpdateSystems.Add(new CollisionDetectionSystem());
            fixedUpdateSystems.Add(new DamageDetectionSystem());

            fixedUpdateSystems.Add(new ActivateSystem());

            fixedUpdateSystems.Add(new PlayerUISystem());

            fixedUpdateSystems.Add(new GameOverSystem());

            fixedUpdateSystems.Init();
        }

        private void DeclareUpdateSystems()
        {
            updateSystems = new EcsSystems(world);

            updateSystems.Add(new PlayerAnimationSystem());
            updateSystems.Add(new PlayerGroundMovementSystem());
            updateSystems.Add(new PlayerGroundRotationSystem());
            updateSystems.Add(new PlayerAirMovementSystem());
            updateSystems.Add(new PlayerAirRotationSystem());

            updateSystems.Init();
        }

        private void DeclareLateUpdateSystems()
        {
            lateUpdateSystems = new EcsSystems(world);

            lateUpdateSystems.Add(new FollowCameraSystem());

            lateUpdateSystems.Init();
        }

        private void FixedUpdate()
            => fixedUpdateSystems.Run();

        private void Update()
            => updateSystems.Run();

        private void LateUpdate()
            => lateUpdateSystems.Run();

        private void OnDestroy()
        {
            initUpdateSystems.Destroy();
            fixedUpdateSystems.Destroy();
            updateSystems.Destroy();
            lateUpdateSystems.Destroy();

            world.Destroy();
        }
    }
}
