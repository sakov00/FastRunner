using System;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Components.Object
{
    public class GravityProvider : MonoProvider<GravityComponent> { }

    [Serializable]
    public struct GravityComponent
    {
        public float GravityValue;
    }
}
