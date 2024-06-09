using UnityEngine;

namespace Assets._Project.Scripts.Components.Unit
{
    internal struct UnitMovementComponent
    {
        public float RunningSpeed;
        public float JumpHeight;
        public float GravityValue;
        public float RunningSpeedLeftRightOnFlying;

        public Vector3 Movement;

        public Transform Transform;
        public CharacterController CharacterController;
    }
}
