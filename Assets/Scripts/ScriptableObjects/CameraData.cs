using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "CameraData", menuName = "DefaultData/CameraData")]

    public class CameraData : ScriptableObject
    {
        public float SmoothValue;

        public float LookingSpeed;

        public Vector3 OffsetInAir;

        public Vector3 OffsetInGround;
    }
}
