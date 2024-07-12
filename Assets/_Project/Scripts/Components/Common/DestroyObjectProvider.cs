using System;
using UnityEngine;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Components.Common
{
    public class DestroyObjectProvider : MonoProvider<DestroyObjectComponent> { }

    [Serializable]
    public struct DestroyObjectComponent
    {
        public GameObject Effect;
        public bool IsActivateDestroy;
        public float DestroyTime;
        [NonSerialized] public float CurrentTime;
    }
}
