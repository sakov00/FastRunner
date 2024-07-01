using Assets._Project.Scripts.Factories;
using System;

namespace Assets._Project.Scripts.Components.Object
{
    [Serializable]
    public struct PooledComponent
    {
        public ObjectPool ObjectPool;
        public bool IsActive;
    }
}
