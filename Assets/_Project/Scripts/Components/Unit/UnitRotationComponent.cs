using System;
using UnityEngine;

namespace Assets._Project.Scripts.Components.Unit
{
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
