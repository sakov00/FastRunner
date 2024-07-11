using Assets._Project.Scripts.Factories;
using System;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Components.Object
{
    public sealed class PooledProvider : MonoProvider<PooledComponent> { }

    [Serializable]
    public struct PooledComponent
    {
        public ObjectPool ObjectPool;
        public bool IsActive;
    }
}
