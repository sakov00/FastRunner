using Assets._Project.Scripts.Components.Object;
using UnityEngine;
using Voody.UniLeo;

namespace Assets._Project.Scripts.ComponentProviders.Object
{
    public sealed class TransformProvider : MonoProvider<TransformComponent> 
    {
        private void Awake()
        {
            value.transform = GetComponent<Transform>();
        }
    }
}
