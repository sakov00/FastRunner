using Assets._Project.Scripts.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Assets._Project.Scripts.InjectInstallers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private GameObject playerManagerSounds;
        [SerializeField] private PlayerData playerData;
        [SerializeField] private CameraData cameraData;

        public override void InstallBindings()
        {
            BindPlayer();
            BindConfigs();
        }

        private void BindPlayer()
        {
            var obj = Container.InstantiatePrefab(playerManagerSounds);
            Container.BindInstance(obj.GetComponent<PlayerManagerSounds>()).AsSingle();
        }

        private void BindConfigs()
        {
            Container.BindInstance(playerData).AsSingle();
            Container.BindInstance(cameraData).AsSingle();
        }
    }
}
