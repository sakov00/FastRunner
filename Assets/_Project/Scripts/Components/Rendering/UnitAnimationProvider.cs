using System;
using UnityEngine;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Components.Rendering
{
    public sealed class UnitAnimationProvider : MonoProvider<UnitAnimationComponent>
    {
        private void Awake()
        {
            value.Animator = GetComponent<Animator>();
        }
    }

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
