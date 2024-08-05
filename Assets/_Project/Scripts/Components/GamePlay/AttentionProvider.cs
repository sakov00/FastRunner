using Assets._Project.Scripts.Enums;
using Leopotam.Ecs;
using System;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Components.GamePlay
{
    public sealed class AttentionProvider : MonoProvider<AttentionComponent> { }

    [Serializable]
    public struct AttentionComponent
    {
        public SpawnEffectType Effect;
    }
}
