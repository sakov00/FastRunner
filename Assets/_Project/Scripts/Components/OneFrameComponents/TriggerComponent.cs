using Assets._Project.Scripts.Enums;
using Leopotam.Ecs;

namespace Assets._Project.Scripts.Components.OneFrameComponents
{
    public struct TriggerComponent
    {
        public EcsEntity SourceEntity;
        public EcsEntity TargetEntity;
        public TriggerEventType eventType;
    }
}
