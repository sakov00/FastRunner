using UnityEngine;

namespace Assets._Project.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "CameraData", menuName = "DefaultData/CameraData")]

    public class CameraData : ScriptableObject
    {
        [Range(1, 10)] public float SmoothValue;

        [Range(1, 10)] public float DistanceFromGround;

        public Vector3 Offset;
    }
}
