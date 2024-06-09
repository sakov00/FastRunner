using Assets._Project.Scripts.Components.Object;
using Assets._Project.Scripts.Components.Player;
using Leopotam.Ecs;
using UnityEngine.SceneManagement;

namespace Assets._Project.Scripts.Systems.Player
{
    internal class GameOverSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerComponent, HealthComponent> filter = null;

        public void Run()
        {
            foreach (var entityIndex in filter)
            {
                ref var playerComponent = ref filter.Get1(entityIndex);
                ref var healthComponent = ref filter.Get2(entityIndex);

                if (healthComponent.HealthPoints == 0)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }
    }
}
