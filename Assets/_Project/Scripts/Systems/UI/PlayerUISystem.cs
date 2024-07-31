using Assets._Project.Scripts.Components.GamePlay;
using Assets._Project.Scripts.Components.UI;
using Leopotam.Ecs;

namespace Assets._Project.Scripts.Systems.UI
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

                if (cameraUIComponent.HealthSlider == null && cameraUIComponent.EnergySlider == null)
                    continue;

                cameraUIComponent.HealthSlider.value = healthComponent.HealthPoints;
                cameraUIComponent.EnergySlider.value = abilityComponent.EnergyPoints;
            }
        }
    }
}
