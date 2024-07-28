using System;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Components.GamePlay
{
    public sealed class DamageProvider : MonoProvider<DamageComponent> { }

    [Serializable]
    public struct DamageComponent
    {
        public float HealthValue;
        public float EnergyValue;
    }
}
