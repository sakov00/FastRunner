using Leopotam.Ecs;
using System;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Components.Hierarchy
{
    public sealed class ChildProvider : MonoProvider<ChildComponent> { }

    public struct ChildComponent
    {
        public EcsEntity ParentEntities;
    }
}
