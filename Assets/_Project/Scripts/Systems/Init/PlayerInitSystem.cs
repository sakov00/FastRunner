using Assets._Project.Scripts.Factories;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace Assets._Project.Scripts.Systems.Init
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        private PlayerFactory playerFactory;

        [Inject]
        private void Construct(PlayerFactory playerFactory)
        {
            this.playerFactory = playerFactory;
        }

        public void Init()
        {
            playerFactory.CreatePlayer(Vector3.zero);
        }
    }
}
