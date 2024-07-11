using System;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Components.Object
{
    public sealed class DamageProvider : MonoProvider<DamageComponent> { }

    [Serializable]
    public struct DamageComponent
    {
        public float Value;
    }
}
