using Assets._Project.Scripts.Components.OneFrameComponents;
using Assets._Project.Scripts.Components.Rendering;
using Leopotam.Ecs;

namespace Assets._Project.Scripts.Systems.Rendering
{
    public class EffectSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ParticleSystemComponent, ActivateComponent> filter = null;

        public void Run()
        {
            foreach (var entityIndex in filter)
            {
                ref var particleSystemComponent = ref filter.Get1(entityIndex);
                ref var activateComponent = ref filter.Get2(entityIndex);

                if(activateComponent.IsActivated)
                    particleSystemComponent.ParticleSystem.Play();
                else
                    particleSystemComponent.ParticleSystem.Stop();
            }
        }
    }
}
