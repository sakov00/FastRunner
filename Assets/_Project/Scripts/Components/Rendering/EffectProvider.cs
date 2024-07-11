using System;
using UnityEngine;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Components.Effect
{
    public sealed class EffectProvider : MonoProvider<EffectComponent> { }

    [Serializable]
    public struct EffectComponent
    {
        public GameObject Effect;

        public bool IsActivateEffect;
    }
}
