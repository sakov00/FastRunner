using Leopotam.Ecs;
using System;
using System.Collections.Generic;

namespace Assets._Project.Scripts.Components.Object
{
    [Serializable]
    public struct CollisionComponent
    {
        public List<EcsEntity> CollisionEntity;
    }
}
