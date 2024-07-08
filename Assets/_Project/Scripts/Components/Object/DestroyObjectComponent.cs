using System;
using UnityEngine;

namespace Assets._Project.Scripts.Components.Object
{
    [Serializable]
    public struct DestroyObjectComponent
    {
        public ParticleSystem ParticleSystem;
        public bool IsActivateDestroy;
    }
}
