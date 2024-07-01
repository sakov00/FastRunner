using Assets._Project.Scripts.Components.Camera;
using UnityEngine;
using Voody.UniLeo;

namespace Assets._Project.Scripts.ComponentProviders.Camera
{
    public sealed class CameraMovementComponentProvider : MonoProvider<CameraMovementComponent>
    {
        private void Awake()
        {
            value.Transform = GetComponent<Transform>();
        }
    }
}
