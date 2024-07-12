using System;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Components.GamePlay
{
    public sealed class CameraMovementProvider : MonoProvider<CameraMovementComponent> { }

    [Serializable]
    public struct CameraMovementComponent
    {
        public float SmoothValue;
    }
}
