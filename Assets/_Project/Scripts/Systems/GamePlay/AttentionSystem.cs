using Assets._Project.Scripts.Components.Common;
using Assets._Project.Scripts.Components.GamePlay;
using Assets._Project.Scripts.UsefullScripts;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets._Project.Scripts.Systems.GamePlay
{
    public class AttentionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TransformComponent, GameObjectComponent, AttentionComponent> filter = null;

        public void Run()
        {
            foreach (var entityIndex in filter)
            {
                ref var transformComponent = ref filter.Get1(entityIndex);
                ref var gameObjectComponent = ref filter.Get2(entityIndex);
                ref var attentionComponent = ref filter.Get3(entityIndex);

                if (!attentionComponent.CreatedEntity.IsAlive() && gameObjectComponent.GameObject.activeInHierarchy)
                {
                    RaycastHit hit;
                    if (UnityEngine.Physics.Raycast(transformComponent.transform.position, Vector3.down, out hit, 100))
                    {
                        var attentionMark = GameObject.Instantiate(attentionComponent.ObjectMark, hit.point, attentionComponent.ObjectMark.transform.rotation);
                        var entity = GameObjectToEntity.AddEntity(attentionMark);
                        attentionComponent.CreatedEntity = entity;
                    }
                }
            }
        }
    }
}
