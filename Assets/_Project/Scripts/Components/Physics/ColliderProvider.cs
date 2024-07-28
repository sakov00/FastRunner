using System;
using UnityEngine;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Components.Physics
{
    public sealed class ColliderProvider : MonoProvider<ColliderComponent>
    {
        private void Awake()
        {
            value.Collider = GetComponent<Collider>();
        }
    }

    [Serializable]
    public struct ColliderComponent
    {
        [NonSerialized] public Collider Collider;
    }
}
