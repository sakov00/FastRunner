using System;
using UnityEngine;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Components.Rendering
{
    public sealed class EffectProvider : MonoProvider<EffectsComponent> { }

    [Serializable]
    public struct EffectsComponent
    {
        public GameObject[] Effects;
    }
}
