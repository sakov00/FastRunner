using UnityEngine;

namespace Assets._Project.Scripts.Components.Unit
{
    internal struct UnitAnimationComponent
    {
        public Animator Animator;

        public string IsGrounded;
        public string IsFalling;
        public string IsJump;
        public string InputZ;
        public string InputX;
    }
}
