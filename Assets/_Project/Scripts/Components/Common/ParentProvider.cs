using Leopotam.Ecs;
using System.Collections.Generic;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Components.Common
{
    public sealed class ParentProvider : MonoProvider<ParentComponent> { }

    public struct ParentComponent
    {
        public List<EcsEntity> ListChildEntities;
    }
}
