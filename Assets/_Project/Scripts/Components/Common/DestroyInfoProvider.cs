using Assets._Project.Scripts.Enums;
using System;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Components.Common
{
    public class DestroyInfoProvider : MonoProvider<DestroyInfoComponent> { }

    [Serializable]
    public struct DestroyInfoComponent
    {
        public SpawnEffectType Effect;
        public float DestroyTime;
        public bool IsContactDestroyed;
        [NonSerialized] public float CurrentTime;
    }
}
