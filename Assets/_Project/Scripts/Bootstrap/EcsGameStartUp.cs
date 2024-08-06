using Assets._Project.Scripts.Components.Common;
using Assets._Project.Scripts.Components.OneFrameComponents;
using Assets._Project.Scripts.Factories;
using Assets._Project.Scripts.Interfaces;
using Assets._Project.Scripts.Systems.Common;
using Assets._Project.Scripts.Systems.GamePlay;
using Assets._Project.Scripts.Systems.GamePlay.Abilities;
using Assets._Project.Scripts.Systems.GamePlay.InputDevice;
using Assets._Project.Scripts.Systems.Rendering;
using Assets._Project.Scripts.Systems.UI;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;
using Zenject;

namespace Assets._Project.Scripts.Bootstrap
{
    public class EcsGameStartUp : MonoBehaviour, ICustomInitializable
    {
        [Inject] private SpawnObjectsFactory spawnObjectsFactory;
        [Inject] private EffectsFactory effectsFactory;

        private EcsWorld world;

        private EcsSystems initUpdateSystems;
        private EcsSystems fixedUpdateSystems;
        private EcsSystems updateSystems;
        private EcsSystems lateUpdateSystems;

        public void Initialize()
        {
            world = new EcsWorld();
            WorldHandler.Init(world);

            QualitySettings.vSyncCount = 1;
            Application.targetFrameRate = 999;

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

            fixedUpdateSystems.Add(new HealthSystem());//need optimization

            fixedUpdateSystems.Add(new TriggerDamageSystem());
            fixedUpdateSystems.Add(new CollisionDamageDetectionSystem());

            fixedUpdateSystems.Add(new ActivateSpawner());
            fixedUpdateSystems.Add(new SpawnerSystem());

            fixedUpdateSystems.Add(new TimerDestroySystem());//need optimization
            fixedUpdateSystems.Add(new TriggerDestroySystem());
            fixedUpdateSystems.Add(new DestroyObjectSystem());

            fixedUpdateSystems.Add(new AttentionSystem());

            fixedUpdateSystems.Add(new ActivateObjectsSystem());

            fixedUpdateSystems.Add(new EffectSystem());

            fixedUpdateSystems.Add(new GameOverSystem());

            fixedUpdateSystems.Add(new GettingEnergySystem());

            fixedUpdateSystems.OneFrame<LandingComponent>();
            fixedUpdateSystems.OneFrame<TriggerComponent>();
            fixedUpdateSystems.OneFrame<CollisionComponent>();
            fixedUpdateSystems.OneFrame<ActivateComponent>();
            fixedUpdateSystems.OneFrame<ActivateDestroyComponent>();

            fixedUpdateSystems.Inject(spawnObjectsFactory); 
            fixedUpdateSystems.Inject(effectsFactory);

            fixedUpdateSystems.Init();
        }

        private void DeclareUpdateSystems()
        {
            updateSystems = new EcsSystems(world);

            //updateSystems.Add(new GravitySystem());//need optimization
            updateSystems.Add(new UnitAnimationSystem());
            updateSystems.Add(new PlayerGroundMovementSystem());
            updateSystems.Add(new PlayerGroundRotationSystem());
            updateSystems.Add(new PlayerAirMovementSystem());
            updateSystems.Add(new PlayerAirRotationSystem());

            //updateSystems.Add(new StoneEffectsSystem());//need optimization

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
            => fixedUpdateSystems?.Run();

        private void Update()
            => updateSystems?.Run();

        private void LateUpdate()
            => lateUpdateSystems?.Run();

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
