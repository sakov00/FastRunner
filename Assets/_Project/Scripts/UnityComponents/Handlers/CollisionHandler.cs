using Assets._Project.Scripts.Components.OneFrameComponents;
using Assets._Project.Scripts.Interfaces;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;
using Zenject;

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

            if (otherEntity != EcsEntity.Null)
            {
                ref var entityCollision = ref _entity.Get<CollisionComponent>();
                entityCollision.SourceEntity = _entity;
                entityCollision.TargetEntity = otherEntity.Value;
            }
        }
    }
}
