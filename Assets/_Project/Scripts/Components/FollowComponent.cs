using Leopotam.Ecs;
using UnityEngine;

namespace Assets._Project.Scripts.Components
{
    internal struct FollowComponent
    {
        public Vector3 Offset;

        public EcsEntity Entity;
    }
}
