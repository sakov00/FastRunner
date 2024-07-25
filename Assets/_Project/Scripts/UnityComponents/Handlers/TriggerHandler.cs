﻿using Assets._Project.Scripts.Components.OneFrameComponents;
using Assets._Project.Scripts.Enums;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Assets._Project.Scripts.UnityComponents.Handlers
{
    public class TriggerHandler : MonoBehaviour
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

        private void OnTriggerEnter(Collider other)
        {
            var otherEntity = other.GetComponent<ConvertToEntity>().TryGetEntity();
            if (otherEntity != EcsEntity.Null)
            {
                CreateTriggerEvent(otherEntity, TriggerEventType.Enter);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var otherEntity = other.GetComponent<ConvertToEntity>().TryGetEntity();
            if (otherEntity != EcsEntity.Null)
            {
                CreateTriggerEvent(otherEntity, TriggerEventType.Exit);
            }
        }

        private void CreateTriggerEvent(EcsEntity? otherEntity, TriggerEventType eventType)
        {
            if (!otherEntity.HasValue)
                return;

            ref var trigger = ref otherEntity.Value.Get<TriggerComponent>();
            trigger.SourceEntity = otherEntity.Value;
            trigger.TargetEntity = _entity;
            trigger.eventType = eventType;
        }
    }
}