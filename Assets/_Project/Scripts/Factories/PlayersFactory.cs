using Assets._Project.Scripts.Components.GamePlay;
using Assets._Project.Scripts.Components.UI;
using Assets._Project.Scripts.Interfaces;
using Assets._Project.Scripts.UseFullScripts;
using Leopotam.Ecs;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace Assets._Project.Scripts.Factories
{
    public class PlayersFactory
    {
        private DiContainer _container;
        private Object _playerPrefab;
        private Object _playerCameraPrefab;

        public PlayersFactory(DiContainer container)
        {
            _container = container;
            LoadResources();
        }

        public void LoadResources()
        {
            _playerPrefab = Resources.Load("Player");
            _playerCameraPrefab = Resources.Load("PlayerCamera");
        }

        public void CreatePlayer(Vector3 position)
        {
            var player = PhotonNetwork.Instantiate(_playerPrefab.name, position, Quaternion.identity);
            var ecsEntityPlayer = InjectAndInitialize(player);

            var playerPhotonView = player.GetComponent<PhotonView>();
            if (playerPhotonView.Owner.IsLocal)
            {
                var playerCamera = (GameObject)GameObject.Instantiate(_playerCameraPrefab, position, Quaternion.identity);
                var ecsEntityPlayerCamera = InjectAndInitialize(playerCamera);

                ref var cameraFollowComponent = ref ecsEntityPlayerCamera.Get<FollowComponent>();
                cameraFollowComponent.Transform = player.transform;

                ref var playerUIComponent = ref ecsEntityPlayer.Get<CameraUIComponent>();
                ref var cameraUIComponent = ref ecsEntityPlayerCamera.Get<CameraUIComponent>();
                playerUIComponent.EnergySlider = cameraUIComponent.EnergySlider;
                playerUIComponent.HealthSlider = cameraUIComponent.HealthSlider;
                playerUIComponent.AttentionImage = cameraUIComponent.AttentionImage;
            }
        }

        private EcsEntity InjectAndInitialize(GameObject gameObject)
        {
            _container.Inject(gameObject);

            var ecsEntity = GameObjectToEntity.AddEntity(gameObject);

            foreach (var component in gameObject.GetComponents<MonoBehaviour>())
            {
                _container.Inject(component);
                if (component is ICustomInitializable initializable)
                {
                    initializable.Initialize();
                }
            }
            return ecsEntity;
        }
    }
}
