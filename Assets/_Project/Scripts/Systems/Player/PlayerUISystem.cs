using Assets._Project.Scripts.Components.Camera;
using Assets._Project.Scripts.Components.Object;
using Assets._Project.Scripts.Components.Player;
using Leopotam.Ecs;

namespace Assets._Project.Scripts.Systems.Player
{
    internal class PlayerUISystem : IEcsRunSystem
    {
        private readonly EcsFilter<CameraUIComponent, HealthComponent, AbilityComponent> filter = null;
        public void Run()
        {
            foreach (var indexEntity in filter)
            {
                ref var cameraUIComponent = ref filter.Get1(indexEntity);
                ref var healthComponent = ref filter.Get2(indexEntity);
                ref var abilityComponent = ref filter.Get3(indexEntity);

                cameraUIComponent.HealthSlider.value = healthComponent.HealthPoints;
                cameraUIComponent.EnergySlider.value = abilityComponent.EnergyPoints;
            }
        }
    }
}
