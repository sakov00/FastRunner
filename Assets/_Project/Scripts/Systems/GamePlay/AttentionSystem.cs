using Assets._Project.Scripts.Components.Common;
using Assets._Project.Scripts.Components.GamePlay;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets._Project.Scripts.Systems.GamePlay
{
    public class AttentionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TransformComponent, AttentionComponent> filter = null;

        public void Run()
        {
            foreach (var entity in filter)
            {
                ref var transformComponent = ref filter.Get1(entity);
                ref var attentionComponent = ref filter.Get2(entity);

                if (!attentionComponent.IsActive)
                {
                    RaycastHit hit;
                    if (UnityEngine.Physics.Raycast(transformComponent.transform.position, Vector3.down, out hit, 100))
                    {
                        var attentionMark = GameObject.Instantiate(attentionComponent.ObjectMark, hit.point, attentionComponent.ObjectMark.transform.rotation);
                        attentionComponent.IsActive = true;
                    }
                }
            }
        }
    }
}
