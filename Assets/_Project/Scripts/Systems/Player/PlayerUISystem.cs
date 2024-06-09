using Assets._Project.Scripts.Components;
using Assets._Project.Scripts.Components.Camera;
using Assets._Project.Scripts.Components.Object;
using Assets._Project.Scripts.Components.Player;
using Leopotam.Ecs;

namespace Assets._Project.Scripts.Systems.Player
{
    internal class PlayerUISystem : IEcsRunSystem
    {
        private readonly EcsFilter<CameraUIComponent, FollowComponent> filter = null;
        public void Run()
        {
            foreach (var indexEntity in filter)
            {
                ref var cameraUIComponent = ref filter.Get1(indexEntity);
                ref var followComponent = ref filter.Get2(indexEntity);

                var healthComponent = followComponent.Entity.Get<HealthComponent>();
                var abilityComponent = followComponent.Entity.Get<AbilityComponent>();

                cameraUIComponent.HealthSlider.value = healthComponent.HealthPoints;
                cameraUIComponent.EnergySlider.value = abilityComponent.EnergyPoints;
            }
        }
    }
}
