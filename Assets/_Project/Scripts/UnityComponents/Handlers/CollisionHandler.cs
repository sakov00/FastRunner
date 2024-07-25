﻿using Assets._Project.Scripts.Components.Physics;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Assets._Project.Scripts.UnityComponents.Handlers
{
    public class CollisionHandler : MonoBehaviour
    {
        private EcsEntity _entity;

        public void FixedUpdate()
        {
            if (_entity == EcsEntity.Null)
            {
                var entity = GetComponent<ConvertToEntity>().TryGetEntity();
                if (GetComponent<ConvertToEntity>().TryGetEntity().HasValue)
                {
                    _entity = entity.Value;
                }
            }
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
