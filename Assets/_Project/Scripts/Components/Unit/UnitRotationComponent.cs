using UnityEngine;

namespace Assets._Project.Scripts.Components.Unit
{
    internal struct UnitRotationComponent
    {
        public float LimitRotationAngleY;

        public float RotationSpeedOnGround;

        public float RotationSensitiveOnGround;

        public float RotationSpeedOnFlying;

        public float RotationSensitiveOnFlying;

        public Quaternion Rotation;

        public Transform Transform;
    }
}
