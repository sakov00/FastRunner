using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Scripts.Components.Object
{
    internal struct CollisionComponent
    {
        public List<EcsEntity> CollisionEntity { get; set; }
        public Collider GameObjectCollider { get; set; }
    }
}
