using System;
using UnityEngine;

namespace Assets._Project.Scripts.Components.Camera
{
    [Serializable]
    public struct CameraMovementComponent
    {
        [NonSerialized] public Transform Transform;

        public float SmoothValue;

        public float DistanceFromGround;
    }
}
