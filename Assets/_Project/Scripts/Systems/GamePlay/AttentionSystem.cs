using Assets._Project.Scripts.Components.Common;
using Assets._Project.Scripts.Components.GamePlay;
using Assets._Project.Scripts.Components.OneFrameComponents;
using Assets._Project.Scripts.Factories;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets._Project.Scripts.Systems.GamePlay
{
    public class AttentionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TransformComponent, GameObjectComponent, AttentionComponent, ActivateComponent> filter = null;

        private EffectsFactory effectsFactory;

        public void Run()
        {
            foreach (var entityIndex in filter)
            {
                ref var transformComponent = ref filter.Get1(entityIndex);
                ref var gameObjectComponent = ref filter.Get2(entityIndex);
                ref var attentionComponent = ref filter.Get3(entityIndex);
                ref var activateComponent = ref filter.Get4(entityIndex);

                if (activateComponent.IsActivated)
                {
                    if (Physics.Raycast(transformComponent.transform.position, Vector3.down, out var hit, 100))
                    {
                        effectsFactory.GetSpawnEffect(attentionComponent.Effect, hit.point);
                    }
                }
            }
        }
    }
}
