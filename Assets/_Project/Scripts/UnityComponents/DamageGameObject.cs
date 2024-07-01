using Assets._Project.Scripts.Components.Object;
using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;
using Voody.UniLeo;

namespace Assets._Project.Scripts.UnityComponents
{
    internal class DamageGameObject : MonoBehaviour
    {
        [SerializeField] float damageValue = 10;

        private void Start()
        {
            var damageEntity = WorldHandler.GetWorld().NewEntity();

            ref var damageComponent = ref damageEntity.Get<DamageComponent>();
            damageComponent.Value = damageValue;

            ref var collisionComponent = ref damageEntity.Get<CollisionComponent>();
            collisionComponent.CollisionEntity = new List<EcsEntity>();

            ref var colliderComponent = ref damageEntity.Get<ColliderComponent>();
            colliderComponent.Collider = GetComponent<Collider>();
        }
    }
}
