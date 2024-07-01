using Assets._Project.Scripts.Components.Object;
using UnityEngine;
using Voody.UniLeo;

namespace Assets._Project.Scripts.ComponentProviders.Object
{
    public sealed class ColliderComponentProvider : MonoProvider<ColliderComponent>
    {
        private void Awake()
        {
            value.Collider = GetComponent<Collider>();
        }
    }
}
