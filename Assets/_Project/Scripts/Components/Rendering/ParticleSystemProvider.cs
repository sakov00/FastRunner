using System;
using UnityEngine;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Components.Rendering
{
    public class ParticleSystemProvider : MonoProvider<ParticleSystemComponent>
    {
        private void Awake()
        {
            value.ParticleSystem = GetComponent<ParticleSystem>();
        }
    }

    [Serializable]
    public struct ParticleSystemComponent
    {
        [NonSerialized] public ParticleSystem ParticleSystem;
    }
}
