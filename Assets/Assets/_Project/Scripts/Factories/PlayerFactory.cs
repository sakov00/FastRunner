using UnityEngine;
using Zenject;

namespace Assets._Project.Scripts.Factories
{
    public class PlayerFactory
    {
        private readonly DiContainer _diContainer;

        private Object _playerPrefab;

        public PlayerFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
            LoadResources();
        }

        public void LoadResources()
        {
            _playerPrefab = Resources.Load("Prefabs/Player");
        }

        public GameObject Create(Vector3 position)
        {
            var player = _diContainer.InstantiatePrefab(_playerPrefab, position, Quaternion.identity, null);
            return player;
        }
    }
}
