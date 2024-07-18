﻿using Assets._Project.Scripts.Components.OneFrameComponents;
using Assets._Project.Scripts.Systems.Common;
using Assets._Project.Scripts.Systems.GamePlay;
using Assets._Project.Scripts.Systems.GamePlay.Abilities;
using Assets._Project.Scripts.Systems.GamePlay.InputDevice;
using Assets._Project.Scripts.Systems.Physics;
using Assets._Project.Scripts.Systems.Rendering;
using Assets._Project.Scripts.Systems.UI;
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

            initUpdateSystems.Init();
        }

        private void DeclareFixedUpdateSystems()
        {
            fixedUpdateSystems = new EcsSystems(world);

            fixedUpdateSystems.ConvertScene();

            fixedUpdateSystems.Add(new PopulateObjectPoolSystem());
            
            fixedUpdateSystems.Add(new ActivateAbilitySystem());
            fixedUpdateSystems.Add(new AccelerationAbilitySystem());
            fixedUpdateSystems.Add(new DoubleJumpAbilitySystem());
            fixedUpdateSystems.Add(new EnergyShieldAbilitySystem());

            fixedUpdateSystems.Add(new HealthSystem());
            fixedUpdateSystems.Add(new DamageDetectionSystem());

            fixedUpdateSystems.Add(new ActivateObjectsSystem());

            fixedUpdateSystems.Add(new SpawnerSystem());

            fixedUpdateSystems.Add(new CleanCollisionSystem());
            fixedUpdateSystems.Add(new CollisionDetectionSystem());

            fixedUpdateSystems.Add(new DestroyObjectSystem());
            fixedUpdateSystems.Add(new GameOverSystem());

            fixedUpdateSystems.Init();
        }

        private void DeclareUpdateSystems()
        {
            updateSystems = new EcsSystems(world);

            updateSystems.Add(new GravitySystem());
            updateSystems.Add(new UnitAnimationSystem());
            updateSystems.Add(new PlayerGroundMovementSystem());
            updateSystems.Add(new PlayerGroundRotationSystem());
            updateSystems.Add(new PlayerAirMovementSystem());
            updateSystems.Add(new PlayerAirRotationSystem());

            updateSystems.Add(new AttentionSystem());
            updateSystems.Add(new EffectSystem());
            updateSystems.Add(new StoneEffectsSystem());

            updateSystems.Add(new GettingEnergySystem());
            updateSystems.OneFrame<LandingComponent>();

            updateSystems.Init();
        }

        private void DeclareLateUpdateSystems()
        {
            lateUpdateSystems = new EcsSystems(world);

            lateUpdateSystems.Add(new FollowCameraSystem());
            lateUpdateSystems.Add(new PlayerUISystem());

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