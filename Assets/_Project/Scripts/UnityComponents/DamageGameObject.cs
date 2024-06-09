using Assets._Project.Scripts.Components.Object;
using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets._Project.Scripts.UnityComponents
{
    internal class DamageGameObject : MonoBehaviour
    {
        private EcsWorld world;
        [SerializeField] float damageValue = 10;

        [Inject]
        private void Construct(EcsWorld world)
        {
            this.world = world;
        }

        private void Start()
        {
            var damageEntity = world.NewEntity();

            ref var damageComponent = ref damageEntity.Get<DamageComponent>();
            damageComponent.Value = damageValue;

            ref var collisionComponent = ref damageEntity.Get<CollisionComponent>();
            collisionComponent.CollisionEntity = new List<EcsEntity>();
            collisionComponent.GameObjectCollider = GetComponent<Collider>();
        }
    }
}
