using Assets._Project.Scripts.Factories;
using UnityEngine;
using Zenject;

namespace Assets._Project.Scripts.Spawners
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private Transform spawnPointPlayer;

        private PlayerFactory playerFactory;

        [Inject]
        public void Contract(PlayerFactory playerFactory)
        {
            this.playerFactory = playerFactory;
        }

        private void Awake()
        {
            SpawnPlayers();
        }

        public void SpawnPlayers()
        {
            playerFactory.Create(spawnPointPlayer.position);
        }
    }
}
