using System;
using UnityEngine;

namespace Assets._Project.Scripts.Components.Object
{
    [Serializable]
    public struct ColliderComponent
    {
        [NonSerialized] public Collider Collider;

        public bool IsCheckCollisions;
    }
}
