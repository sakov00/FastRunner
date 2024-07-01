using System;
using UnityEngine;

namespace Assets._Project.Scripts.Components.Unit
{
    [Serializable]
    public struct UnitAnimationComponent
    {
        [NonSerialized] public Animator Animator;

        public string IsGrounded;
        public string IsFalling;
        public string IsJump;
        public string InputZ;
        public string InputX;
    }
}
