using UnityEngine;

namespace Assets._Project.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ObjectPoolConfig", menuName = "ScriptableObjects/ObjectPoolConfig", order = 1)]
    public class ObjectPoolConfig : ScriptableObject
    {
        public int PortalPoolSize = 10;
        public int ExplosionCactusPoolSize = 10;
        public int SpotPoolSize = 10;
        public int FiredStonePoolSize = 10;
    }
}
