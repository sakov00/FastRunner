using Leopotam.Ecs;
using System;

namespace Assets._Project.Scripts.Components.Object
{
    [Serializable]
    public struct TriggerComponent
    {
        public EcsEntity? SourceEntity;
        public EcsEntity? TargetEntity;
    }
}
