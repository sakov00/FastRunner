﻿using Assets._Project.Scripts.Components.GamePlay;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;
using Zenject;

namespace Assets._Project.Scripts.Factories
{
    public class PlayerFactory
    {
        private Object _playerPrefab;
        private Object _playerCameraPrefab;

        public PlayerFactory()
        {
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
            var playerEntity = WorldHandler.GetWorld().GetFilter(typeof(EcsFilter<PlayerComponent>)).GetEntity(0);

            var cameraGameObject = CreateCameraGameObject(position);
            var cameraEntity = WorldHandler.GetWorld().GetFilter(typeof(EcsFilter<CameraMovementComponent>)).GetEntity(0);

            var player = (PlayerObject: playerObject, PlayerEntity: playerEntity);
            return player;
        }

        private GameObject CreatePlayerGameObject(Vector3 position)
        {
            return (GameObject)Object.Instantiate(_playerPrefab, position, Quaternion.identity);
        }

        public GameObject CreateCameraGameObject(Vector3 position)
        {
            return (GameObject)Object.Instantiate(_playerCameraPrefab, position, Quaternion.identity);
        }
    }
}
