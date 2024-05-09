using Assets._Project.Scripts.Spawners;
using UnityEngine;
using Zenject;

namespace Assets._Project.Scripts.Bootstrap
{
    public class BootstrapScene : MonoBehaviour
    {
        private PlayerSpawner playerSpawner;

        [Inject]
        public void Contract(PlayerSpawner playerSpawner)
        {
            this.playerSpawner = playerSpawner;
        }

        public void Start()
        {
            playerSpawner.SpawnPlayers();
        }
    }
}
