using Assets._Project.Scripts.Factories;

namespace Assets._Project.Scripts.Components.Object
{
    internal struct PooledComponent
    {
        public ObjectPool ObjectPool { get; set; }
        public bool IsActive { get; set; }
    }
}
