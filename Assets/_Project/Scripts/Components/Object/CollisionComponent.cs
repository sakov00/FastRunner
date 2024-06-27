using Leopotam.Ecs;
using System.Collections.Generic;

namespace Assets._Project.Scripts.Components.Object
{
    internal struct CollisionComponent
    {
        public List<EcsEntity> CollisionEntity { get; set; }
    }
}
