using System;
using UnityEngine;

namespace Assets._Project.Scripts.Components.Unit
{
    [Serializable]
    public struct UnitMovementComponent
    {
        public float RunningSpeed;
        public float JumpHeight;
        public float GravityValue;
        public float RunningSpeedLeftRightOnFlying;

        [NonSerialized] public Vector3 Movement;
    }
}
