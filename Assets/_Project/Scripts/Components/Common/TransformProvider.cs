using UnityEngine;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Components.Common
{
    public sealed class TransformProvider : MonoProvider<TransformComponent>
    {
        private void Awake()
        {
            value.transform = GetComponent<Transform>();
        }
    }

    public struct TransformComponent
    {
        public Transform transform;
    }
}
