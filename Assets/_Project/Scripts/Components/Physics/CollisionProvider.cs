using Leopotam.Ecs;
using System.Collections.Generic;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Components.Physics
{
    public sealed class CollisionProvider : MonoProvider<CollisionComponent>
    {
        private void Awake()
        {
            value.CollisionEntity = new List<EcsEntity>();
        }
    }

    public struct CollisionComponent
    {
        public List<EcsEntity> CollisionEntity;
    }
}
