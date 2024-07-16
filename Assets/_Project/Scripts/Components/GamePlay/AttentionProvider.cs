using Leopotam.Ecs;
using System;
using UnityEngine;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Components.GamePlay
{
    public sealed class AttentionProvider : MonoProvider<AttentionComponent> { }

    [Serializable]
    public struct AttentionComponent
    {
        public GameObject ObjectMark;
        public EcsEntity CreatedEntity;
    }
}
