using Assets._Project.Scripts.UseFullScripts;
using System;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Components.Common
{
    public sealed class ObjectPoolProvider : MonoProvider<ObjectPoolComponent>
    {
        private void Awake()
        {
            value.ObjectPool = new ObjectPool();
        }
    }

    [Serializable]
    public struct ObjectPoolComponent
    {
        public ObjectPool ObjectPool;
        public int Size;
    }
}
