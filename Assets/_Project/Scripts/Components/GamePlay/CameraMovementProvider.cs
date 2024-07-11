using System;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Components.Camera
{
    public sealed class CameraMovementProvider : MonoProvider<CameraMovementComponent> { }

    [Serializable]
    public struct CameraMovementComponent
    {
        public float SmoothValue;
    }
}
