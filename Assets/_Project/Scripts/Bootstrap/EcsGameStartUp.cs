using Assets._Project.Scripts.Systems;
using Assets._Project.Scripts.Systems.Ability;
using Assets._Project.Scripts.Systems.Init;
using Assets._Project.Scripts.Systems.Player;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace Assets._Project.Scripts.Bootstrap
{
    public class EcsGameStartUp : MonoBehaviour
    {
        private DiContainer _diContainer;
        private EcsWorld world;

        private EcsSystems fixedUpdateSystems;
        private EcsSystems updateSystems;
        private EcsSystems lateUpdateSystems;

        [Inject]
        private void Contract(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        private void Start()
        {
            world = new EcsWorld();

            fixedUpdateSystems = new EcsSystems(world);
            updateSystems = new EcsSystems(world);
            lateUpdateSystems = new EcsSystems(world);

            updateSystems.Add(_diContainer.Resolve<PlayerInitSystem>());

            if (SystemInfo.deviceType == DeviceType.Desktop)
            {
                updateSystems.Add(new InputPCSystem());
            }
            else if (SystemInfo.deviceType == DeviceType.Handheld)
            {
                updateSystems.Add(new InputMobileSystem());
            }

            updateSystems.Add(new PlayerAnimationSystem());
            updateSystems.Add(new PlayerGroundMovementSystem());
            updateSystems.Add(new PlayerGroundRotationSystem());
            updateSystems.Add(new PlayerAirMovementSystem());
            updateSystems.Add(new PlayerAirRotationSystem());

            fixedUpdateSystems.Add(new PlayerActivateAbilitySystem());
            fixedUpdateSystems.Add(new AccelerationAbilitySystem());
            fixedUpdateSystems.Add(new DoubleJumpAbilitySystem());
            fixedUpdateSystems.Add(new EnergyShieldAbilitySystem());

            lateUpdateSystems.Add(new FollowCameraSystem());

            fixedUpdateSystems.Init();
            updateSystems.Init();
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
            fixedUpdateSystems.Destroy();
            updateSystems.Destroy();
            lateUpdateSystems.Destroy();

            world.Destroy();
        }
    }
}
