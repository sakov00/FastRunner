using System;
using UnityEngine;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Components.Unit
{
    public sealed class UnitRotationProvider : MonoProvider<UnitRotationComponent> { }

    [Serializable]
    public struct UnitRotationComponent
    {
        public float LimitRotationAngleY;

        public float RotationSpeedOnGround;

        public float RotationSensitiveOnGround;

        public float RotationSpeedOnFlying;

        public float RotationSensitiveOnFlying;

        [NonSerialized] public Quaternion Rotation;
    }
}
