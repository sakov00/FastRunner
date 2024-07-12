using System;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Components.GamePlay
{
    public class GravityProvider : MonoProvider<GravityComponent> { }

    [Serializable]
    public struct GravityComponent
    {
        public float GravityValue;

        public float LengthRay;
        public bool IsGrounded;
    }
}
