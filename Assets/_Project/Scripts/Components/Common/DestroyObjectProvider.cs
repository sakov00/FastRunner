using System;
using UnityEngine;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Components.Object
{
    public class DestroyObjectProvider : MonoProvider<DestroyObjectComponent> { }

    [Serializable]
    public struct DestroyObjectComponent
    {
        public ParticleSystem ParticleSystem;
        public bool IsActivateDestroy;
        public float DestroyTime;
        [NonSerialized] public float CurrentTime;
    }
}
