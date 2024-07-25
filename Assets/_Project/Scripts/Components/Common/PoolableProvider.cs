using Assets._Project.Scripts.UsefullScripts;
using System;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Components.Common
{
    public sealed class PoolableProvider : MonoProvider<PoolableComponent> { }

    [Serializable]
    public struct PoolableComponent
    {
        public ObjectPool ObjectPool;
    }
}
