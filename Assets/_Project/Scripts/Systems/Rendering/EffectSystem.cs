using Assets._Project.Scripts.Components.Rendering;
using Leopotam.Ecs;

namespace Assets._Project.Scripts.Systems.Rendering
{
    public class EffectSystem : IEcsRunSystem
    {
        private readonly EcsFilter<EffectsComponent> filter = null;

        public void Run()
        {
            foreach (var entityIndex in filter)
            {
                ref var effectComponent = ref filter.Get1(entityIndex);
                //effectComponent.Effect.SetActive(effectComponent.IsActivateEffect);
            }
        }
    }
}
