using Assets._Project.Scripts.Components.OneFrameComponents;
using Assets._Project.Scripts.Enums;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;
using Zenject;

namespace Assets._Project.Scripts.UnityComponents.Handlers
{
    public class TriggerHandler : MonoBehaviour, IInitializable
    {
        private EcsEntity _entity;

        public void Initialize()
        {
            _entity = GetComponent<ConvertToEntity>().TryGetEntity().Value;
        }

        private void OnTriggerEnter(Collider other)
        {
            var convertToEntity = other.gameObject.GetComponent<ConvertToEntity>();
            if (convertToEntity == null)
                return;

            var otherEntity = convertToEntity.TryGetEntity();
            if (otherEntity != EcsEntity.Null)
            {
                CreateTriggerEvent(otherEntity, TriggerEventType.Enter);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var convertToEntity = other.gameObject.GetComponent<ConvertToEntity>();
            if (convertToEntity == null)
                return;

            var otherEntity = convertToEntity.TryGetEntity();
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
            trigger.SourceEntity = _entity;
            trigger.TargetEntity = otherEntity.Value;
            trigger.eventType = eventType;
        }
    }
}
