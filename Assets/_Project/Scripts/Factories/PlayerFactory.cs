using UnityEngine;
using Zenject;

namespace Assets._Project.Scripts.Factories
{
    public class PlayerFactory
    {
        private readonly DiContainer _diContainer;

        private Object _playerPrefab;
        private Object _playerCameraPrefab;

        public PlayerFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
            LoadResources();
        }

        public void LoadResources()
        {
            _playerPrefab = Resources.Load("Prefabs/Player");
            _playerCameraPrefab = Resources.Load("Prefabs/PlayerCamera");
        }

        public GameObject CreatePlayer(Vector3 position)
        {
            var player = _diContainer.InstantiatePrefab(_playerPrefab, position, Quaternion.identity, null);
            return player;
        }

        public GameObject CreatePlayerCamera(Vector3 position)
        {
            var playerCamera = _diContainer.InstantiatePrefab(_playerCameraPrefab, position, Quaternion.identity, null);
            return playerCamera;
        }
    }
}
