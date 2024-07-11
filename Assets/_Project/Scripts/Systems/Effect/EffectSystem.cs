using Assets._Project.Scripts.Components.Effect;
using Leopotam.Ecs;

namespace Assets._Project.Scripts.Systems.Effect
{
    public class EffectSystem : IEcsRunSystem
    {
        private readonly EcsFilter<EffectComponent> filter = null;

        public void Run()
        {
            foreach (var entityIndex in filter)
            {
                ref var effectComponent = ref filter.Get1(entityIndex);
                effectComponent.Effect.SetActive(effectComponent.IsActivateEffect);
            }
        }
    }
}
