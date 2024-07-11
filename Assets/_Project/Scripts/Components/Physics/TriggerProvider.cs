using Leopotam.Ecs;
using System;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Components.Object
{
    public sealed class TriggerProvider : MonoProvider<TriggerComponent> { }

    [Serializable]
    public struct TriggerComponent
    {
        public EcsEntity? SourceEntity;
        public EcsEntity? TargetEntity;
    }
}
