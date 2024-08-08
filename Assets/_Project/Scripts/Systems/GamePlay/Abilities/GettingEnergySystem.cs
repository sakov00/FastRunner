using Assets._Project.Scripts.Components.Common;
using Assets._Project.Scripts.Components.GamePlay;
using Assets._Project.Scripts.Components.OneFrameComponents;
using Assets._Project.Scripts.Components.Physics;
using Leopotam.Ecs;


namespace Assets._Project.Scripts.Systems.GamePlay.Abilities
{
    public class GettingEnergySystem : IEcsRunSystem
    {
        private readonly EcsFilter<AbilityComponent, CharacterControllerComponent, TransformComponent, GroundedComponent> filter = null;

        public void Run()
        {
            foreach (var entityIndex in filter)
            {
                ref var abilityComponent = ref filter.Get1(entityIndex);
                ref var characterControllerComponent = ref filter.Get2(entityIndex);
                ref var transformComponent = ref filter.Get3(entityIndex);

                var angleX = transformComponent.transform.eulerAngles.x;

                if (angleX > 180f)
                {
                    angleX -= 360f;
                }

                if (angleX > abilityComponent.AngleForGettingEnergy)
                {
                    abilityComponent.EnergyPoints += abilityComponent.GettingEnergyValue;
                }
                if (angleX < -abilityComponent.AngleForGettingEnergy)
                {
                    abilityComponent.EnergyPoints -= abilityComponent.GettingEnergyValue;
                }
            }
        }
    }
}
