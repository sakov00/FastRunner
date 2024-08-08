using Assets._Project.Scripts.Components.OneFrameComponents;
using Assets._Project.Scripts.Interfaces;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Assets._Project.Scripts.UnityComponents.Handlers
{
    public class CollisionHandler : MonoBehaviour, ICustomInitializable
    {
        private EcsEntity _entity;

        public void Initialize()
        {
            _entity = GetComponent<ConvertToEntity>().TryGetEntity().Value;
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            var convertToEntity = hit.gameObject.GetComponent<ConvertToEntity>();
            if (convertToEntity == null)
                return;

            var otherEntity = convertToEntity.TryGetEntity();
            CreateCollisionEvent(otherEntity);
        }

        //void OnCollisionEnter(Collision collision)
        //{
        //    var convertToEntity = collision.gameObject.GetComponent<ConvertToEntity>();
        //    if (convertToEntity == null)
        //        return;

        //    var otherEntity = convertToEntity.TryGetEntity();
        //    CreateCollisionEvent(otherEntity);
        //}

        private void CreateCollisionEvent(EcsEntity? otherEntity)
        {
            ref var entityCollision = ref _entity.Get<CollisionComponent>();
            entityCollision.SourceEntity = _entity;
            entityCollision.TargetEntity = otherEntity.Value;
        }
    }
}
