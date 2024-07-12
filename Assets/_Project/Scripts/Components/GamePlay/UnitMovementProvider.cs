using System;
using UnityEngine;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Components.GamePlay
{
    public sealed class UnitMovementProvider : MonoProvider<UnitMovementComponent> { }

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
